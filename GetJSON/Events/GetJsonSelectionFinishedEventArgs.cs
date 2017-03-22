using ArcGIS.Desktop.Mapping;

namespace GetJSON.Events
{
    public class GetJsonSelectionFinishedEventArgs : System.EventArgs
    {
        public BasicFeatureLayer BasicFL
        {
            get;
            private set;
        }

        public GetJsonSelectionFinishedEventArgs(BasicFeatureLayer bfl)
        {
            this.BasicFL = bfl;
        }
    }
}