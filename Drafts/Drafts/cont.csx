delegate Answer K<T,Answer>(Func<T,Answer> k);

static T CallCC<T>(Func<Func<T,T>, T> f)
