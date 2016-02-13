namespace EssentialTools.Models
{
    public interface IDiscountHelper
    {
        decimal ApplyDiscount(decimal totalParam);
    }
    public class DefaultDiscoutHelper:IDiscountHelper
    {
        private decimal discountSize;

        public DefaultDiscoutHelper(decimal discountParam)
        {
            discountSize = discountParam;
        }
        public decimal ApplyDiscount(decimal totalParam)
        {
            return (totalParam-(discountSize/100m*totalParam));
        }
    }
}