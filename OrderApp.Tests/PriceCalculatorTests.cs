using OrderApp.Core;

namespace OrderApp.Tests;

public class PriceCalculatorTests
{
    [Theory]
    [InlineData(100, 5, false, 500)]   // 未滿千，不打折
    [InlineData(100, 10, false, 900)]  // 剛好滿千 → 9 折（邊界值）
    [InlineData(100, 10, true, 855)]   // 滿千 + VIP → 900 × 0.95
    [InlineData(100, 5, true, 475)]    // 只有 VIP 折扣
    [InlineData(0, 3, false, 0)]       // 免費商品
    public void Calculate_回傳正確金額(int price, int qty, bool isVip, int expected)
    {
        Assert.Equal(expected, PriceCalculator.Calculate(price, qty, isVip));
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    public void 數量小於等於0_應拋出例外(int qty)
    {
        Assert.Throws<ArgumentException>(() => PriceCalculator.Calculate(100, qty, false));
    }

    [Fact]
    public void 單價為負數_應拋出例外()
    {
        Assert.Throws<ArgumentException>(() => PriceCalculator.Calculate(-1, 1, false));
    }
}
