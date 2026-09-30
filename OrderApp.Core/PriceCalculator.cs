namespace OrderApp.Core;

/// <summary>
/// 訂單金額計算。純邏輯、不依賴畫面，所以可以在 CI 裡自動測試。
/// </summary>
public static class PriceCalculator
{
    /// <summary>滿額折扣門檻。</summary>
    public const decimal DiscountThreshold = 1000m;

    public static decimal Calculate(decimal price, int qty, bool isVip)
    {
        if (price < 0) throw new ArgumentException("單價不可為負數", nameof(price));
        if (qty <= 0) throw new ArgumentException("數量必須大於 0", nameof(qty));

        decimal total = price * qty;
        if (total >= DiscountThreshold) total *= 0.9m;   // 滿千打九折
        if (isVip) total *= 0.95m;                       // VIP 再打 95 折
        return Math.Round(total, 0, MidpointRounding.AwayFromZero);
    }
}
