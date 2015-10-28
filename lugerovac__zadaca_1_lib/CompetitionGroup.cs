using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lugerovac__zadaca_1_lib
{
    public class CompetitionGroup
    {
        private Theme theme;
        public Theme Theme
        {
            get { return theme; }
            set { theme = value; }
        }

        private List<string> categories;
        public List<string> Categories
        {
            get { return categories; }
        }

        public Dictionary<string, Photo> PhotoCollection = new Dictionary<string, Photo>();

        public bool AddCategory(string newCategory)
        {
            if (Categories.Contains(newCategory))
                return false;

            categories.Add(newCategory);
            PhotoCollection.Add(newCategory, new Photo());
            return true;
        }

        public bool AddCategory(string newCategory, Photo photo)
        {
            if (Categories.Contains(newCategory))
                return false;

            categories.Add(newCategory);
            PhotoCollection.Add(newCategory, photo);
            return true;
        }
    }
}
