// WARNING: Do not modify! Generated file.

namespace UnityEngine.Purchasing.Security {
    public class GooglePlayTangle
    {
        private static byte[] data = System.Convert.FromBase64String("bSibpENc18enRq0ApI7R8xJXvR/PHgmzRtcunZ+2Hmgpi6SWeT7GdiPD/RMPCWFJ1Wdh77tRLd3rTitgoRVgiDYm7Y+elGargFtyZIoX3ejaGWJ8QiHOCs7gZcR2fP7zejHEwnvwuL55tycGeJ6wbkypdQh23/uyf81ObX9CSUZlyQfJuEJOTk5KT0xkWb924mOQ5lVwHKM0vZLYIBn/cQ1hhPIYxZoI/fdAOjPfxd8NNJsPdKoVugeOY40UqIdDYp9gaN4gdhrBSSDBoFQp8pNH7x6HcnL50pRo+M1OQE9/zU5FTc1OTk/96lEfUK6AeC3QZdve9WLfaMn1sTC/irDwcN2UiW6gtokPAIJmhZGFv3OQ0IycrAuS4lycA4mDBk1MTk9O");
        private static int[] order = new int[] { 2,5,4,8,7,6,7,11,10,10,13,11,12,13,14 };
        private static int key = 79;

        public static readonly bool IsPopulated = true;

        public static byte[] Data() {
        	if (IsPopulated == false)
        		return null;
            return Obfuscator.DeObfuscate(data, order, key);
        }
    }
}
