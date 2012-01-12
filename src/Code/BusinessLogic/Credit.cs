namespace Triangles.Code.BusinessLogic
{
    /// <summary>
    /// Отражает задолженность других партнеров перед партнером
    /// </summary>
    public class Credit
    {
        public string Who { get; set; }
        public decimal Amount { get; set; }
    }
}