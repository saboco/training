#load "Rop.fs"
#load "Library.fs"

open Training.Calculator.Model

let goodCmd = "1 + 2"
parseCommand goodCmd

let badCmd = "nothing"
parseCommand badCmd

let commands = ["1 + 5";  "1 * 4"; "1 / 50"; "9 - 2"]

treatCommands commands
    


