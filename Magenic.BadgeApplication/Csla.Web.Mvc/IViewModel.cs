namespace Csla.Web.Mvc
{
    /// <summary>
    /// Defines a CSLA .NET MVC viewmodel object.
    /// </summary>
    public interface IViewModel
    {
        /// <summary>
        /// Object property for the contained business object
        /// </summary>
        object ModelObject { get; set; }
    }
}
