namespace PocketBook.Model
{
    using System;

    public class User : ICloneable
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }

        public object Clone()
        {
            return this.MemberwiseClone();
        }
    }
}