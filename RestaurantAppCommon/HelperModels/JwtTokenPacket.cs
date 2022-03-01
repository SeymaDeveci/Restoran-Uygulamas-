namespace RestaurantAppCommon.HelperModels
{
    public class JwtTokenPacket
    {
        public string Token { get; set; }
        public string Expiration { get; set; }
        public string UserName { get; set; }
    }
}
