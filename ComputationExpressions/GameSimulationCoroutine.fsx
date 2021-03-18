open System

module Math =
    [<Measure>]
    type m
    [<Measure>]
    type kg
    [<Measure>]
    type s
    [<Measure>]
    type N = kg * m / s^2

    type Vector2<[<Measure>] 'a> = 
        {   X : float<'a>
            Y : float<'a> }        
        static member Zero : Vector2<'a> = { X = 0.0<_>; Y = 0.0<_> }
        static member ( + ) (v1:Vector2<'a>,v2:Vector2<'a>):Vector2<'a> = { X = v1.X + v2.X; Y = v1.Y + v2.Y }
        static member ( + ) (v:Vector2<'a>,k:float<'a>):Vector2<'a> = { X = v.X + k; Y = v.Y + k }
        static member ( + ) (k:float<'a>,v:Vector2<'a>):Vector2<'a> = v + k
        static member ( ~- ) (v:Vector2<'a>):Vector2<'a> = { X = -v.X; Y = -v.Y }
        static member ( - ) (v1:Vector2<'a>,v2:Vector2<'a>):Vector2<'a> = v1 + (-v2)
        static member ( - ) (v:Vector2<'a>,k:float<'a>):Vector2<'a> = v + (-k)
        static member ( - ) (k:float<'a>,v:Vector2<'a>):Vector2<'a> = k + (-v)
        static member ( * ) (v1:Vector2<'a>,v2:Vector2<'b>):Vector2<'a * 'b> = { X = v1.X * v2.X; Y = v1.Y * v2.Y }
        static member ( * ) (v:Vector2<'a>,f:float<'b>):Vector2<'a * 'b> = { X = v.X * f; Y = v.Y * f }
        static member ( * ) (f:float<'b>,v:Vector2<'a>):Vector2<'b * 'a> = { X = f * v.X; Y = f * v.Y }
        static member ( / ) (v:Vector2<'a>,f:float<'b>):Vector2<'a / 'b> = v * (1.0 / f)
        static member Distance(v1:Vector2<'a>,v2:Vector2<'a>) = (v1-v2).Length
        static member Normalize(v:Vector2<'a>):Vector2<1> = v / v.Length
        static member Dot(v1:Vector2<'a>,v2:Vector2<'a>) = v1.X * v2.X + v1.Y * v2.Y
        member this.Length : float<'a> = sqrt((this.X * this.X + this.Y * this.Y))
        member this.Normalized = this / this.Length
        

module Coroutines =

    type Coroutine<'a> = unit -> CoroutineStep<'a>
        and CoroutineStep<'a> =
          | Return of 'a
          | Yield of Coroutine<'a>
          | ArrowYield of Coroutine<'a>

    let rec bind (f : 'a -> Coroutine<'b>) (m : Coroutine<'a>) : Coroutine<'b> =
            fun s ->
              match m s with
              | Return x -> f x s
              | Yield m' -> Yield (bind f m')
              | ArrowYield m' -> ArrowYield (bind f m')

    let ret x  = fun () -> Return x

    type CoroutineBuilder() =
        member __.Return(x:'a) : Coroutine<'a> = ret x
        member this.ReturnFrom(s:Coroutine<'a>) = s
        member this.Bind(p, k) : Coroutine<'b> = bind k p
        member this.Zero() : Coroutine<Unit> = this.Return ()
        member this.Delay s =  s()
        member this.Run s = s
        member this.Combine(p1:Coroutine<'a>, p2:Coroutine<'b>) : Coroutine<'b> =
            this.Bind(p1, fun _ -> p2)

    let co = CoroutineBuilder()

    let yield' : Coroutine<Unit> = fun () -> Yield(fun () -> Return())
    let arrowyield : Coroutine<Unit> = fun () -> ArrowYield(fun () -> Return())

    let ignore' (s:Coroutine<'a>) : Coroutine<Unit> =
          co{
            let! _ = s
            return ()
          }

    let rec repeat (s:Coroutine<Unit>) : Coroutine<Unit> =
          co{
            do! s
            return! repeat s
          }

    let rec (.||) (s1:Coroutine<'a>) (s2:Coroutine<'b>) : Coroutine<Choice<'a,'b>> =
          fun s ->
            match s1 s,s2 s with
            | Return x, _        -> Return(Choice1Of2 x)
            | _, Return y        -> Return(Choice2Of2 y)
            | ArrowYield k1, _   ->
              co{
                let! res = k1
                return Choice1Of2 res
              } |> Yield
            | _, ArrowYield k2   ->
              co{
                let! res = k2
                return Choice2Of2 res
              } |> Yield
            | Yield k1, Yield k2 -> (.||) k1 k2 |> Yield

    let (.||>) s1 s2 = ignore' (s1 .|| s2)

    let rec (=>) (c:Coroutine<bool>) (s:Coroutine<'a>) : Coroutine<'a> =
          co{
            let! x = c
            if x then
              do! arrowyield
              let! res = s
              return res
            else
              do! yield'
              return! (=>) c s
          }

    let wait_doing (action:float -> Coroutine<Unit>) (interval:float) : Coroutine<Unit> =
          let time : Coroutine<DateTime> = fun _ -> Return(DateTime.Now)
          co{
            let! t0 = time
            let rec wait() =
              co{
                  let! t = time
                  let dt = (t - t0).TotalSeconds
                  if dt < interval then
                    do! yield'
                    do! action dt
                    return! wait()
              }
            do! wait()
          }

    let wait = wait_doing (fun (dt:float) -> co{ return () }) 

    let co_step = function
        | Return x          -> co{ return x }
        | Yield k           -> k
        | ArrowYield k      -> k

module PoliceChase = 
    open Coroutines
    open Math

    [<Measure>]
    type Life

    type Ship =
        {
            mutable Position      : Vector2<m>
            mutable Velocity      : Vector2<m/s>
            DryMass               : float<kg>
            mutable Fuel          : float<kg>
            MaxFuel               : float<kg>
            Thrust                : float<N/s>
            FuelBurn              : float<kg/s>
            mutable Force         : Vector2<N>
            mutable Integrity     : float<Life>
            MaxIntegrity          : float<Life>
            Damage                : float<Life/s>
            WeaponsRange          : float<m>
            mutable AI            : Coroutine<Unit> }

        member this.Mass = this.DryMass + this.Fuel


    type Station = { 
        Position      : Vector2<m> }

    type PoliceChase = {
        PoliceStation : Station
        Patrol        : Ship
        Pirate        : Ship
        Cargo         : Ship }

    let dt = 180.0<s>
    let field_size = 3.8e7<m>

    let impulse (self:Ship) (dir:Vector2<1>) (engine_power:float) =
      if self.Fuel > self.FuelBurn * engine_power * dt then
        do self.Force <- self.Thrust * dir * engine_power * dt
        do self.Fuel <- self.Fuel - self.FuelBurn * engine_power * dt
    
    let attack (self:Ship) (target:Ship) =
      co{
        do! yield'
        let dir = Vector2<_>.Normalize(target.Position - self.Position)
        let dist = (target.Position - self.Position).Length
        if dist > self.WeaponsRange * 0.8 then
          if self.Velocity.Length > 0.01<_> then
            let v_norm = self.Velocity.Normalized
            let dot = Vector2.Dot(dir,v_norm)
            if dot <= 0.0 then
              do impulse self (-self.Velocity.Normalized) 1.0
            elif dot < 0.5 then
              do impulse self (Vector2<1>.Normalize((-(self.Velocity.Normalized - dir * dot)))) 0.3
            else
              do impulse self dir 0.1
            do! wait 1.0
          else
            do impulse self dir 1.0
            do! wait 1.0
        return ()
      }

    let reach_station (self:Ship) (s:PoliceChase) =
      co{
        do! yield'
        let dir = Vector2<_>.Normalize(s.PoliceStation.Position - self.Position)
        if Vector2<_>.Distance(s.PoliceStation.Position, self.Position) <= field_size * 1.0e-1 then
          let zero_velocity =
            co{
              do! yield'
              return self.Velocity <- Vector2<_>.Zero
            }
          do! wait_doing (fun _ -> zero_velocity) 5.0
          do self.Integrity <- self.MaxIntegrity
          do self.Fuel <- self.MaxFuel
        elif self.Velocity.Length > 0.01<_> then
          let dot = Vector2<1>.Dot(self.Velocity.Normalized,dir)
          if dot <= 0.0 then
            do impulse self (-self.Velocity.Normalized) 1.0
          elif dot < 0.5 then
            do impulse self (Vector2<1>.Normalize((-(self.Velocity.Normalized - dir * dot)))) 0.3
          else
            do impulse self dir 0.2
          do! wait 1.0
        else
          do impulse self dir 1.0
          do! wait 1.0
        return ()
      }


    let patrol_ai (s:PoliceChase) =
        let self = s.Patrol
        let healthy_and_fueled =
            co{
              do! yield'
              return self.Integrity > self.MaxIntegrity * 0.4 && self.Fuel > self.MaxFuel * 0.4
            }
        let need_docking =
            co{
              do! yield'
              let! h = healthy_and_fueled
              return not h
            }
        repeat ((healthy_and_fueled => attack self (s.Pirate)) .||>
               (need_docking       => reach_station self s))

    let pirate_ai (s:PoliceChase) =
      let self = s.Pirate
      let patrol_near =
        co{
          do! yield'
          return Vector2<_>.Distance(self.Position,s.Patrol.Position) < s.Patrol.WeaponsRange
        }
      let patrol_far =
        co{
          let! n = patrol_near
          return not n
        }
      repeat ((patrol_near => (attack (s.Pirate) (s.Patrol))) .||>
               (patrol_far  => (attack (s.Pirate) (s.Cargo))))

    let cargo_ai (s:PoliceChase) =
      let self = s.Cargo
      co{
        do! yield'
        do! reach_station self s
      } |> repeat


    let s0() =
        let s =
            {
              PoliceStation = { Position = { X = field_size; Y = field_size } * 0.25 }
              Patrol        =
                {
                  Position        = { X = field_size; Y = field_size } * 0.25
                  Velocity        = Vector2<_>.Zero
                  DryMass         = 4.5e4<_>
                  Fuel            = 2.2e6<_>
                  MaxFuel         = 2.2e6<_>
                  FuelBurn        = 2.2e6<_> / (50.0 * 180.0)
                  Thrust          = 5.0e6<_> / 180.0
                  Force           = Vector2<_>.Zero
                  Integrity       = 100.0<_>
                  MaxIntegrity    = 100.0<_>
                  Damage          = 1.0e-1<_> / 180.0
                  WeaponsRange    = field_size * 0.1
                  AI              = co{ return () }
                }
              Pirate        =
                {
                  Position        = { X = field_size; Y = field_size } * 0.75
                  Velocity        = Vector2<_>.Zero
                  DryMass         = 3.0e4<_>
                  Fuel            = 2.2e6<_>
                  MaxFuel         = 2.2e6<_>
                  FuelBurn        = 2.2e6<_> / (30.0 * 180.0)
                  Thrust          = 5.0e5<_> / 180.0
                  Force           = Vector2<_>.Zero
                  Integrity       = 75.0<_>
                  MaxIntegrity    = 75.0<_>
                  Damage          = 2.0e-1<_> / 180.0
                  WeaponsRange    = field_size * 0.15
                  AI              = co{ return () }
                }
              Cargo        =
                {
                  Position        = { X = field_size; Y = field_size  * 0.7 } * 0.7
                  Velocity        = Vector2<_>.Zero
                  DryMass         = 2.3e6<_>
                  Fuel            = 3.5e8<_> * 0.3
                  MaxFuel         = 3.5e8<_>
                  FuelBurn        = 3.5e6<_> / 180.0
                  Thrust          = 3.4e6<_> / 180.0
                  Force           = Vector2<_>.Zero
                  Integrity       = 300.0<_>
                  MaxIntegrity    = 300.0<_>
                  Damage          = 1.0e-3<_> / 180.0
                  WeaponsRange    = field_size * 0.1
                  AI              = co{ return () }
                }
            }
        do s.Patrol.AI <- patrol_ai s
        do s.Pirate.AI <- pirate_ai s
        do s.Cargo.AI  <- cargo_ai s
        s

    let ship_step (s:Ship) =
        do s.Position <- s.Position + s.Velocity * dt
        do s.Velocity <- s.Velocity + dt * s.Force / s.Mass
        do s.Force    <- Vector2<_>.Zero
        do s.AI       <- co_step (s.AI())


    let simulation_step (s:PoliceChase) =
      do ship_step s.Patrol
      do ship_step s.Pirate
      do ship_step s.Cargo
      if Vector2<_>.Distance(s.Patrol.Position, s.Pirate.Position) < s.Patrol.WeaponsRange then
        do s.Pirate.Integrity <- s.Pirate.Integrity - s.Patrol.Damage * dt
      if Vector2<_>.Distance(s.Cargo.Position, s.Pirate.Position) < s.Cargo.WeaponsRange then
        do s.Pirate.Integrity <- s.Pirate.Integrity - s.Cargo.Damage * dt
      if Vector2<_>.Distance(s.Patrol.Position, s.Pirate.Position) < s.Pirate.WeaponsRange then
        do s.Patrol.Integrity <- s.Patrol.Integrity - s.Pirate.Damage * dt
      elif Vector2<_>.Distance(s.Cargo.Position, s.Pirate.Position) < s.Pirate.WeaponsRange then
        do s.Cargo.Integrity <- s.Cargo.Integrity - s.Pirate.Damage * dt

    let print(s:PoliceChase) =
      do Console.Clear()
      let set_cursor (v:Vector2<_>) =
        Console.SetCursorPosition((((v.X / field_size) * 79.0) |> int) - 1 |> max 0 |> min 79, ((v.Y / field_size) * 23.0) |> int |> max 0 |> min 23)
      let set_cursor_on_ship (s:Ship) = set_cursor (s.Position)
      let set_cursor_on_station (s:Station) = set_cursor (s.Position)
      do set_cursor_on_station (s.PoliceStation)
      do Console.Write("¤")
      do set_cursor_on_ship (s.Patrol)
      let ship_fuel (s:Ship) =
        (9.0 * s.Fuel / s.MaxFuel).ToString("#.")
      let ship_integrity (s:Ship) =
        (9.0 * s.Integrity / s.MaxIntegrity).ToString("#.")
      do Console.Write((ship_fuel s.Patrol) + "∆" + (ship_integrity s.Patrol))
      do set_cursor_on_ship (s.Pirate)
      do Console.Write((ship_fuel s.Pirate) + "†" + (ship_integrity s.Pirate))
      do set_cursor_on_ship (s.Cargo)
      do Console.Write((ship_fuel s.Cargo) + "•" + (ship_integrity s.Cargo))
      do Console.SetCursorPosition(0,0)
      do System.Threading.Thread.Sleep(10)

    let simulation() =
      let s = s0()
      let rec simulation() =
        do print s
        if s.Patrol.Integrity > 0.0<_> && s.Pirate.Integrity > 0.0<_> && s.Cargo.Integrity > 0.0<_> then
          do simulation (simulation_step s)
      do simulation()



PoliceChase.simulation()

