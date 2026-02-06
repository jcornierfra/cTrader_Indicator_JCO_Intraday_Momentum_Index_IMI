# JCO - Intraday Momentum Index (IMI)

A cTrader indicator implementing the **Intraday Momentum Index (IMI)**, an oscillator developed by Tushar Chande.

## Description

The IMI works on the same principle as the **Relative Strength Index (RSI)** — it oscillates between 0 and 100 with overbought and oversold levels — but its calculation differs fundamentally:

- The **RSI** measures momentum based on **Close-to-Close** price changes (comparing each bar's close to the previous bar's close).
- The **IMI** measures momentum based on the **Open-to-Close** relationship within each bar (intraday candle body).

This makes the IMI particularly useful for identifying **intraday buying and selling pressure** by analyzing whether candles are closing above or below their open over a given period.

### Formula

```
IMI = 100 × UpSum / (UpSum + DownSum)
```

Where, over the lookback period:
- **UpSum** = sum of (Close - Open) for bars where Close > Open (bullish candles)
- **DownSum** = sum of (Open - Close) for bars where Close < Open (bearish candles)

### Interpretation

| Zone | Value | Signal |
|------|-------|--------|
| Overbought | IMI > 70 | Potential selling pressure ahead |
| Neutral | 30 < IMI < 70 | No clear directional bias |
| Oversold | IMI < 30 | Potential buying pressure ahead |

## Parameters

| Parameter | Default | Description |
|-----------|---------|-------------|
| Length | 7 | Lookback period for the calculation |
| Overbought Level | 70 | Upper threshold level |
| Oversold Level | 30 | Lower threshold level |

## Installation

1. Download the `.cs` file
2. In cTrader, go to **Automate** > **Indicators**
3. Click **New Indicator**, replace the code with the downloaded file, and **Build**

## Changelog

### v1.0 (2026-02-06)
- Intraday Momentum Index calculation (Close vs Open)
- Configurable period length
- Overbought/Oversold levels with midline

## License

This project is open source and available for personal and commercial use.
