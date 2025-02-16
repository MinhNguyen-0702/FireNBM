namespace FireNBM.Pattern
{
    /// <summary>
    ///     
    /// </summary>
    public class IMessage
    {
        public string NameType;
        public IMessage() { NameType = this.GetType().Name; }
    }
}