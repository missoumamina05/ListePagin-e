namespace ListePaginée
{
    class Chat
    {
        public int Id;
        public string Name = "";
        public override string ToString() => $"{Id}:{Name}";
    }

    class Program
    {
        static void Main(string[] args)
        {
            var liste = new ListePaginee<Chat>(3);

            liste.Ajouter(new Chat { Id = 1, Name = "Mittens" });
            liste.Ajouter(new Chat { Id = 2, Name = "Whiskers" });
            liste.Ajouter(new Chat { Id = 3, Name = "Shadow" });
            liste.Ajouter(new Chat { Id = 4, Name = "Luna" });
            liste.Ajouter(new Chat { Id = 5, Name = "Mittens" });
            liste.Ajouter(new Chat { Id = 6, Name = "Snowball" });
            liste.Ajouter(new Chat { Id = 7, Name = "Tiger" });

            PrintState(liste, "Après ajouts");

            var found = liste.RechercherPar(c => c.Name == "Mittens");
            Console.WriteLine($"Trouvé {found.Count} occurrence(s) de 'Mittens'.");
            foreach (var p in found)
            {
                var chat = p.Page.Items[p.Index];
                Console.WriteLine($" - page {p.PageNumber}, index {p.Index} -> {chat}");
            }

            if (found.Count > 0)
            {
                liste.Supprimer(found[0]);
                Console.WriteLine("Suppression de la première occurrence effectuée.");
            }

            PrintState(liste, "Après suppression");

            liste.Compacter();
            PrintState(liste, "Après compactage");
        }

        static void PrintState(ListePaginee<Chat> liste, string titre)
        {
            Console.WriteLine($"=== {titre} ===");
            Console.WriteLine($"Éléments: {liste.CompterElements()}  |  Pages: {liste.CompterPages()}");

            var items = liste.GetAllItems();
            foreach (var it in items)
                Console.WriteLine($"page {it.PageNumber} index {it.Index} -> {it.Value}");

            Console.WriteLine();
        }
    }
}
