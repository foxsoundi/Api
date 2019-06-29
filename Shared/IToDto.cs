namespace Shared
{
    internal interface IToDto<out T>
    {
        T GetDto();
    }
}