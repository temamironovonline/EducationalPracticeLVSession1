using System.Windows.Media;

namespace EducationalPracticeLVSession1
{
    partial class Products
    {
        public Brush BackgroundColor
        {
            get
            {
                if (ProductCurrentDiscount > 15)
                {
                    BrushConverter bc = new BrushConverter();
                    return (Brush)bc.ConvertFrom("#7fff00");
                }
                else
                {
                    return null;
                }
            }
        }
    }
}
