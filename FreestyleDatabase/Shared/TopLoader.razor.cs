using System.Threading.Tasks;

namespace FreestyleDatabase.Shared
{
    partial class TopLoader
    {
        public bool IsVisible { get; private set; }

        public void Hide()
        {
            IsVisible = false;
        }

        public void Show()
        {
            IsVisible = true;
        }
    }
}