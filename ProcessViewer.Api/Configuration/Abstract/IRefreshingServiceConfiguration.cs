namespace ProcessViewer.Api.Configuration.Abstract
{
    /// <summary>   Interface for refreshing service configuration. </summary>
    public interface IRefreshingServiceConfiguration
    {
        /// <summary>   Gets or sets the refresh period of processes list. </summary>
        ///
        /// <value> The refresh period. </value>

        int ProcessesListRefreshPeriod { get; set; }
    }
}