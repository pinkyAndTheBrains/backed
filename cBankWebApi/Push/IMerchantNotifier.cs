namespace cBankWebApi.Push
{
    public interface IMerchantNotifier
    {
        void NotifyMerchant(MerchantNotificationMessage message);
    }
}