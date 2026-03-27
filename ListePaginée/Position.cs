namespace ListePaginée
{
    public class Position<T>
    {
        public Page<T> Page { get; }
        public int Index { get; }
        public int PageNumber { get; }

        public Position(Page<T> page, int index, int pageNumber = -1)
        {
            Page = page ?? throw new System.ArgumentNullException(nameof(page));
            Index = index;
            PageNumber = pageNumber;
        }
    }
}


