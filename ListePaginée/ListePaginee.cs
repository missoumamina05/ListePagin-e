namespace ListePaginée
{
    public class ListePaginee<T>
    {
        private Page<T>? premierePage;
        private int taillePage;

        public ListePaginee(int taillePage)
        {
            if (taillePage <= 0)
                throw new ArgumentException("La taille de page doit être > 0", nameof(taillePage));

            this.taillePage = taillePage;
            premierePage = null;
        }

        public void Ajouter(T valeur)
        {
            if (premierePage == null)
            {
                premierePage = new Page<T>(taillePage);
                premierePage.Items[0] = valeur;
                premierePage.Used[0] = true;
                return;
            }

            Page<T> page = premierePage;

            while (page != null)
            {
                for (int i = 0; i < taillePage; i++)
                {
                    if (!page.Used[i])
                    {
                        page.Items[i] = valeur;
                        page.Used[i] = true;
                        return;
                    }
                }

                if (page.Next == null)
                    break;

                page = page.Next;
            }

            Page<T> nouvelle = new Page<T>(taillePage);
            nouvelle.Items[0] = valeur;
            nouvelle.Used[0] = true;
            page.Next = nouvelle;
        }

        public Position<T>? Rechercher(T valeur)
        {
            Page<T>? page = premierePage;
            int pageIndex = 0;

            while (page != null)
            {
                for (int i = 0; i < taillePage; i++)
                {
                    if (page.Used[i] && System.Collections.Generic.EqualityComparer<T>.Default.Equals(page.Items[i], valeur))
                    {
                        var p = new Position<T>(page, i, pageIndex + 1);
                        return p;
                    }
                }

                page = page.Next;
                pageIndex++;
            }

            return null;
        }

        public List<Position<T>> RechercherTous(T valeur)
        {
            List<Position<T>> result = new List<Position<T>>();
            Page<T>? page = premierePage;
            int pageIndex = 0;

            while (page != null)
            {
                for (int i = 0; i < taillePage; i++)
                {
                    if (page.Used[i] && System.Collections.Generic.EqualityComparer<T>.Default.Equals(page.Items[i], valeur))
                    {
                        var p = new Position<T>(page, i, pageIndex + 1);
                        result.Add(p);
                    }
                }

                page = page.Next;
                pageIndex++;
            }

            return result;
        }

        
        public List<Position<T>> RechercherPar(Predicate<T> pred)
        {
            var result = new List<Position<T>>();
            Page<T> page = premierePage;
            int pageIndex = 0;

            while (page != null)
            {
                for (int i = 0; i < taillePage; i++)
                {
                    if (page.Used[i] && pred(page.Items[i]))
                    {
                        result.Add(new Position<T>(page, i, pageIndex + 1));
                    }
                }

                page = page.Next;
                pageIndex++;
            }

            return result;
        }

        public List<(T Value, int PageNumber, int Index)> GetAllItems()
        {
            var list = new List<(T, int, int)>();
            Page<T>? page = premierePage;
            int pageIndex = 1;

            while (page != null)
            {
                for (int i = 0; i < taillePage; i++)
                {
                    if (page.Used[i])
                        list.Add((page.Items[i], pageIndex, i));
                }

                page = page.Next;
                pageIndex++;
            }

            return list;
        }

        public void Supprimer(Position<T>? pos)
        {
            if (pos == null) return;

            pos.Page.Used[pos.Index] = false;
            pos.Page.Items[pos.Index] = default(T);
        }

        public void Compacter()
        {
            List<T> elements = new List<T>();
            Page<T> page = premierePage;

            while (page != null)
            {
                for (int i = 0; i < taillePage; i++)
                {
                    if (page.Used[i])
                        elements.Add(page.Items[i]);
                }

                page = page.Next;
            }

            premierePage = null;

            foreach (T val in elements)
                Ajouter(val);
        }

        public int CompterElements()
        {
            int count = 0;
            Page<T> page = premierePage;

            while (page != null)
            {
                for (int i = 0; i < taillePage; i++)
                {
                    if (page.Used[i])
                        count++;
                }

                page = page.Next;
            }

            return count;
        }

        public int CompterPages()
        {
            int count = 0;
            Page<T> page = premierePage;

            while (page != null)
            {
                count++;
                page = page.Next;
            }

            return count;
        }
    }
}
