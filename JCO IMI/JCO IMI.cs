// =====================================================
// JCO - Intraday Momentum Index (IMI) Indicator
// =====================================================
// Version: 1.0
// Date: 2026-02-06
// GitHub: https://github.com/jcornierfra/cTrader-Indicator-JCO-IMI
//
// Changelog:
// v1.0 (2026-02-06)
//   - Intraday Momentum Index calculation (Close vs Open)
//   - Configurable period length
//   - Overbought/Oversold levels with midline
// =====================================================

using cAlgo.API;
using cAlgo.API.Indicators;

namespace cAlgo.Indicators
{
    [Indicator(IsOverlay = false, TimeZone = TimeZones.UTC, AccessRights = AccessRights.None)]
    public class IntradayMomentumIndex : Indicator
    {
        [Parameter("Length", DefaultValue = 7, MinValue = 1)]
        public int Length { get; set; }

        [Parameter("Overbought Level", DefaultValue = 70)]
        public double OverboughtLevel { get; set; }

        [Parameter("Oversold Level", DefaultValue = 30)]
        public double OversoldLevel { get; set; }

        [Output("IMI", LineColor = "DarkOrange", PlotType = PlotType.Line, LineStyle = LineStyle.Solid, Thickness = 2)]
        public IndicatorDataSeries ImiResult { get; set; }

        [Output("Overbought", LineColor = "DeepSkyBlue", PlotType = PlotType.Line, LineStyle = LineStyle.Solid, Thickness = 1)]
        public IndicatorDataSeries Overbought { get; set; }

        [Output("Midline", LineColor = "Gray", PlotType = PlotType.Line, LineStyle = LineStyle.Solid, Thickness = 1)]
        public IndicatorDataSeries Midline { get; set; }

        [Output("Oversold", LineColor = "DeepSkyBlue", PlotType = PlotType.Line, LineStyle = LineStyle.Solid, Thickness = 1)]
        public IndicatorDataSeries Oversold { get; set; }

        protected override void Initialize()
        {
        }

        public override void Calculate(int index)
        {
            // Somme des gains et pertes sur la période
            double upSum = 0;
            double downSum = 0;

            for (int i = 0; i < Length; i++)
            {
                if (index - i < 0)
                    break;

                double periodGain = Bars.ClosePrices[index - i] > Bars.OpenPrices[index - i] 
                    ? Bars.ClosePrices[index - i] - Bars.OpenPrices[index - i] 
                    : 0;

                double periodLoss = Bars.ClosePrices[index - i] > Bars.OpenPrices[index - i] 
                    ? 0 
                    : Bars.OpenPrices[index - i] - Bars.ClosePrices[index - i];

                upSum += periodGain;
                downSum += periodLoss;
            }

            // Calcul de l'IMI
            double totalSum = upSum + downSum;
            ImiResult[index] = totalSum > 0 ? 100 * upSum / totalSum : 50;

            // Lignes horizontales
            Overbought[index] = OverboughtLevel;
            Midline[index] = 50;
            Oversold[index] = OversoldLevel;
        }
    }
}