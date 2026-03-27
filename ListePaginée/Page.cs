namespace ListePaginée
{
   
    public class Page<T>
    {
        public T[] Items { get; }
        public bool[] Used { get; }
        public Page<T>? Next { get; set; }

        public Page(int taille)
        {
            if (taille <= 0) throw new ArgumentException("La taille doit être > 0", nameof(taille));
            Items = new T[taille];
            Used = new bool[taille];
            Next = null;
        }
    }
}
