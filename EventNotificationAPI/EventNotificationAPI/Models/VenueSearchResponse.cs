using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EventNotificationAPI.Models
{
    /// NOTE: Generated code may require at least .NET Framework 4.5 or .NET Core/Standard 2.0.
    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    [System.Xml.Serialization.XmlRootAttribute(Namespace = "", IsNullable = false)]
    public partial class VenueSearchResponse
    {

        private ushort total_itemsField;

        private long page_sizeField;

        private ushort page_countField;

        private long page_numberField;

        private object page_itemsField;

        private object first_itemField;

        private object last_itemField;

        private decimal search_timeField;

        private searchVenue[] venuesField;

        private decimal versionField;

        /// <remarks/>
        public ushort total_items
        {
            get
            {
                return this.total_itemsField;
            }
            set
            {
                this.total_itemsField = value;
            }
        }

        /// <remarks/>
        public long page_size
        {
            get
            {
                return this.page_sizeField;
            }
            set
            {
                this.page_sizeField = value;
            }
        }

        /// <remarks/>
        public ushort page_count
        {
            get
            {
                return this.page_countField;
            }
            set
            {
                this.page_countField = value;
            }
        }

        /// <remarks/>
        public long page_number
        {
            get
            {
                return this.page_numberField;
            }
            set
            {
                this.page_numberField = value;
            }
        }

        /// <remarks/>
        public object page_items
        {
            get
            {
                return this.page_itemsField;
            }
            set
            {
                this.page_itemsField = value;
            }
        }

        /// <remarks/>
        public object first_item
        {
            get
            {
                return this.first_itemField;
            }
            set
            {
                this.first_itemField = value;
            }
        }

        /// <remarks/>
        public object last_item
        {
            get
            {
                return this.last_itemField;
            }
            set
            {
                this.last_itemField = value;
            }
        }

        /// <remarks/>
        public decimal search_time
        {
            get
            {
                return this.search_timeField;
            }
            set
            {
                this.search_timeField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlArrayItemAttribute("venue", IsNullable = false)]
        public searchVenue[] venues
        {
            get
            {
                return this.venuesField;
            }
            set
            {
                this.venuesField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public decimal version
        {
            get
            {
                return this.versionField;
            }
            set
            {
                this.versionField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class searchVenue
    {

        private object urlField;

        private string nameField;

        private string venue_nameField;

        private string descriptionField;

        private string venue_typeField;

        private string addressField;

        private string city_nameField;

        private string region_nameField;

        private string region_abbrField;

        private object postal_codeField;

        private string country_nameField;

        private string country_abbr2Field;

        private string country_abbrField;

        private decimal longitudeField;

        private decimal latitudeField;

        private string geocode_typeField;

        private object ownerField;

        private object timezoneField;

        private object createdField;

        private long event_countField;

        private long trackback_countField;

        private long comment_countField;

        private long link_countField;

        private object imageField;

        private string idField;

        /// <remarks/>
        public object url
        {
            get
            {
                return this.urlField;
            }
            set
            {
                this.urlField = value;
            }
        }

        /// <remarks/>
        public string name
        {
            get
            {
                return this.nameField;
            }
            set
            {
                this.nameField = value;
            }
        }

        /// <remarks/>
        public string venue_name
        {
            get
            {
                return this.venue_nameField;
            }
            set
            {
                this.venue_nameField = value;
            }
        }

        /// <remarks/>
        public string description
        {
            get
            {
                return this.descriptionField;
            }
            set
            {
                this.descriptionField = value;
            }
        }

        /// <remarks/>
        public string venue_type
        {
            get
            {
                return this.venue_typeField;
            }
            set
            {
                this.venue_typeField = value;
            }
        }

        /// <remarks/>
        public string address
        {
            get
            {
                return this.addressField;
            }
            set
            {
                this.addressField = value;
            }
        }

        /// <remarks/>
        public string city_name
        {
            get
            {
                return this.city_nameField;
            }
            set
            {
                this.city_nameField = value;
            }
        }

        /// <remarks/>
        public string region_name
        {
            get
            {
                return this.region_nameField;
            }
            set
            {
                this.region_nameField = value;
            }
        }

        /// <remarks/>
        public string region_abbr
        {
            get
            {
                return this.region_abbrField;
            }
            set
            {
                this.region_abbrField = value;
            }
        }

        /// <remarks/>
        public object postal_code
        {
            get
            {
                return this.postal_codeField;
            }
            set
            {
                this.postal_codeField = value;
            }
        }

        /// <remarks/>
        public string country_name
        {
            get
            {
                return this.country_nameField;
            }
            set
            {
                this.country_nameField = value;
            }
        }

        /// <remarks/>
        public string country_abbr2
        {
            get
            {
                return this.country_abbr2Field;
            }
            set
            {
                this.country_abbr2Field = value;
            }
        }

        /// <remarks/>
        public string country_abbr
        {
            get
            {
                return this.country_abbrField;
            }
            set
            {
                this.country_abbrField = value;
            }
        }

        /// <remarks/>
        public decimal longitude
        {
            get
            {
                return this.longitudeField;
            }
            set
            {
                this.longitudeField = value;
            }
        }

        /// <remarks/>
        public decimal latitude
        {
            get
            {
                return this.latitudeField;
            }
            set
            {
                this.latitudeField = value;
            }
        }

        /// <remarks/>
        public string geocode_type
        {
            get
            {
                return this.geocode_typeField;
            }
            set
            {
                this.geocode_typeField = value;
            }
        }

        /// <remarks/>
        public object owner
        {
            get
            {
                return this.ownerField;
            }
            set
            {
                this.ownerField = value;
            }
        }

        /// <remarks/>
        public object timezone
        {
            get
            {
                return this.timezoneField;
            }
            set
            {
                this.timezoneField = value;
            }
        }

        /// <remarks/>
        public object created
        {
            get
            {
                return this.createdField;
            }
            set
            {
                this.createdField = value;
            }
        }

        /// <remarks/>
        public long event_count
        {
            get
            {
                return this.event_countField;
            }
            set
            {
                this.event_countField = value;
            }
        }

        /// <remarks/>
        public long trackback_count
        {
            get
            {
                return this.trackback_countField;
            }
            set
            {
                this.trackback_countField = value;
            }
        }

        /// <remarks/>
        public long comment_count
        {
            get
            {
                return this.comment_countField;
            }
            set
            {
                this.comment_countField = value;
            }
        }

        /// <remarks/>
        public long link_count
        {
            get
            {
                return this.link_countField;
            }
            set
            {
                this.link_countField = value;
            }
        }

        /// <remarks/>
        public object image
        {
            get
            {
                return this.imageField;
            }
            set
            {
                this.imageField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string id
        {
            get
            {
                return this.idField;
            }
            set
            {
                this.idField = value;
            }
        }
    }

}