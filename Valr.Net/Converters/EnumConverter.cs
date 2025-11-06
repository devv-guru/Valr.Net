using CryptoExchange.Net.SharedApis.Enums;
using Valr.Net.Enums;

namespace Valr.Net.Converters
{
    public static class EnumConverter
    {
        public static SharedOrderSide ConvertToSharedOrderSide(ValrOrderSide side)
        {
            if (side == ValrOrderSide.Buy)
                return SharedOrderSide.Buy;

            return SharedOrderSide.Sell;
        }

        public static SharedOrderStatus ConvertToSharedOrderStatus(ValrOrderStatus status)
        {
            switch (status)
            {
                case ValrOrderStatus.New:
                case ValrOrderStatus.Open:
                case ValrOrderStatus.PartiallyFilled:
                case ValrOrderStatus.Active: return SharedOrderStatus.Open;
                case ValrOrderStatus.Failed:
                case ValrOrderStatus.Cancelled: return SharedOrderStatus.Canceled;
                case ValrOrderStatus.Filled: return SharedOrderStatus.Filled;
                default: return SharedOrderStatus.Open;
            }
        }

        public static SharedOrderType ConvertToSharedOrderType(ValrOrderType type)
        {
            switch (type)
            {
                case ValrOrderType.LIMIT:
                case ValrOrderType.LIMIT_POST_ONLY: return SharedOrderType.Limit;
                case ValrOrderType.SIMPLE:
                case ValrOrderType.MARKET: return SharedOrderType.Market;
                default: return SharedOrderType.Other;
            }
        }

        public static ValrOrderSide ConvertFromSharedOrderSide(SharedOrderSide side)
        {
            if (side == SharedOrderSide.Buy)
                return ValrOrderSide.Buy;

            return ValrOrderSide.Sell;
        }

        public static SharedOrderStatus ConvertFromSharedOrderStatus(ValrOrderStatus status)
        {
            switch (status)
            {
                case ValrOrderStatus.New:
                case ValrOrderStatus.Open:
                case ValrOrderStatus.PartiallyFilled:
                case ValrOrderStatus.Active: return SharedOrderStatus.Open;
                case ValrOrderStatus.Failed:
                case ValrOrderStatus.Cancelled: return SharedOrderStatus.Canceled;
                case ValrOrderStatus.Filled: return SharedOrderStatus.Filled;
                default: return SharedOrderStatus.Open;
            }
        }

        public static SharedOrderType ConvertFromSharedOrderType(ValrOrderType type)
        {
            switch (type)
            {
                case ValrOrderType.LIMIT:
                case ValrOrderType.LIMIT_POST_ONLY: return SharedOrderType.Limit;
                case ValrOrderType.SIMPLE:
                case ValrOrderType.MARKET: return SharedOrderType.Market;
                default: return SharedOrderType.Other;
            }
        }
    }
}
