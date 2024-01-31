namespace User.Domain
{
    public enum CheckCodeResult
    {
        OK, PhoneNumberNotFound, Lockout, CodeError
    }
}
