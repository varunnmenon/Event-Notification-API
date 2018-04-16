using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml.Serialization;

namespace EventNotificationAPI.Models
{
        // NOTE: Generated code may require at least .NET Framework 4.5 or .NET Core/Standard 2.0.
        /// <remarks/>
        [System.SerializableAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
        [System.Xml.Serialization.XmlRootAttribute(Namespace = "", IsNullable = true)]
        public partial class SearchEventResp
    {

            private ushort total_itemsField;

            private long page_sizeField;

            private long page_countField;

            private long page_numberField;

            private object page_itemsField;

            private object first_itemField;

            private object last_itemField;

            private decimal search_timeField;

            private searchEvent[] eventsField;

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
            public long page_count
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
            [System.Xml.Serialization.XmlArrayItemAttribute("event", IsNullable = false)]
            public searchEvent[] events
            {
                get
                {
                    return this.eventsField;
                }
                set
                {
                    this.eventsField = value;
                }
            }
        }

        /// <remarks/>
        [System.SerializableAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
        public partial class searchEvent
        {

            private string titleField;

            private string urlField;

            private string descriptionField;

            private string start_timeField;

            private object stop_timeField;

            private object tz_idField;

            private object tz_olson_pathField;

            private object tz_countryField;

            private object tz_cityField;

            private string olson_pathField;

            private string venue_idField;

            private string venue_urlField;

            private string venue_nameField;

            private long venue_displayField;

            private string venue_addressField;

            private string city_nameField;

            private string region_nameField;

            private string region_abbrField;

            private string postal_codeField;

            private string country_nameField;

            private string country_abbr2Field;

            private string country_abbrField;

            private decimal latitudeField;

            private decimal longitudeField;

            private string geocode_typeField;

            private long all_dayField;

            private string recur_stringField;

            private object calendar_countField;

            private object comment_countField;

            private object link_countField;

            private object going_countField;

            private object watching_countField;

            private string createdField;

            private string ownerField;

            private string modifiedField;

            private searchEventPerformer[] performersField;

            private searchEventImage imageField;

            private long privacyField;

            private object calendarsField;

            private object groupsField;

            private object goingField;

            private string idField;

            /// <remarks/>
            public string title
            {
                get
                {
                    return this.titleField;
                }
                set
                {
                    this.titleField = value;
                }
            }

            /// <remarks/>
            public string url
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
            public string start_time
            {
                get
                {
                    return this.start_timeField;
                }
                set
                {
                    this.start_timeField = value;
                }
            }

            /// <remarks/>
            public object stop_time
            {
                get
                {
                    return this.stop_timeField;
                }
                set
                {
                    this.stop_timeField = value;
                }
            }

            /// <remarks/>
            public object tz_id
            {
                get
                {
                    return this.tz_idField;
                }
                set
                {
                    this.tz_idField = value;
                }
            }

            /// <remarks/>
            public object tz_olson_path
            {
                get
                {
                    return this.tz_olson_pathField;
                }
                set
                {
                    this.tz_olson_pathField = value;
                }
            }

            /// <remarks/>
            public object tz_country
            {
                get
                {
                    return this.tz_countryField;
                }
                set
                {
                    this.tz_countryField = value;
                }
            }

            /// <remarks/>
            public object tz_city
            {
                get
                {
                    return this.tz_cityField;
                }
                set
                {
                    this.tz_cityField = value;
                }
            }

            /// <remarks/>
            public string olson_path
            {
                get
                {
                    return this.olson_pathField;
                }
                set
                {
                    this.olson_pathField = value;
                }
            }

            /// <remarks/>
            public string venue_id
            {
                get
                {
                    return this.venue_idField;
                }
                set
                {
                    this.venue_idField = value;
                }
            }

            /// <remarks/>
            public string venue_url
            {
                get
                {
                    return this.venue_urlField;
                }
                set
                {
                    this.venue_urlField = value;
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
            public long venue_display
            {
                get
                {
                    return this.venue_displayField;
                }
                set
                {
                    this.venue_displayField = value;
                }
            }

            /// <remarks/>
            public string venue_address
            {
                get
                {
                    return this.venue_addressField;
                }
                set
                {
                    this.venue_addressField = value;
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
            public string postal_code
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
            public long all_day
            {
                get
                {
                    return this.all_dayField;
                }
                set
                {
                    this.all_dayField = value;
                }
            }

            /// <remarks/>
            public string recur_string
            {
                get
                {
                    return this.recur_stringField;
                }
                set
                {
                    this.recur_stringField = value;
                }
            }

            /// <remarks/>
            public object calendar_count
            {
                get
                {
                    return this.calendar_countField;
                }
                set
                {
                    this.calendar_countField = value;
                }
            }

            /// <remarks/>
            public object comment_count
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
            public object link_count
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
            public object going_count
            {
                get
                {
                    return this.going_countField;
                }
                set
                {
                    this.going_countField = value;
                }
            }

            /// <remarks/>
            public object watching_count
            {
                get
                {
                    return this.watching_countField;
                }
                set
                {
                    this.watching_countField = value;
                }
            }

            /// <remarks/>
            public string created
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
            public string owner
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
            public string modified
            {
                get
                {
                    return this.modifiedField;
                }
                set
                {
                    this.modifiedField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlArrayItemAttribute("performer", IsNullable = false)]
            public searchEventPerformer[] performers
            {
                get
                {
                    return this.performersField;
                }
                set
                {
                    this.performersField = value;
                }
            }

            /// <remarks/>
            public searchEventImage image
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
            public long privacy
            {
                get
                {
                    return this.privacyField;
                }
                set
                {
                    this.privacyField = value;
                }
            }

            /// <remarks/>
            public object calendars
            {
                get
                {
                    return this.calendarsField;
                }
                set
                {
                    this.calendarsField = value;
                }
            }

            /// <remarks/>
            public object groups
            {
                get
                {
                    return this.groupsField;
                }
                set
                {
                    this.groupsField = value;
                }
            }

            /// <remarks/>
            public object going
            {
                get
                {
                    return this.goingField;
                }
                set
                {
                    this.goingField = value;
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

        /// <remarks/>
        [System.SerializableAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
        public partial class searchEventPerformer
        {

            private string idField;

            private string urlField;

            private string nameField;

            private string short_bioField;

            private string creatorField;

            private string linkerField;

            /// <remarks/>
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

            /// <remarks/>
            public string url
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
            public string short_bio
            {
                get
                {
                    return this.short_bioField;
                }
                set
                {
                    this.short_bioField = value;
                }
            }

            /// <remarks/>
            public string creator
            {
                get
                {
                    return this.creatorField;
                }
                set
                {
                    this.creatorField = value;
                }
            }

            /// <remarks/>
            public string linker
            {
                get
                {
                    return this.linkerField;
                }
                set
                {
                    this.linkerField = value;
                }
            }
        }

        /// <remarks/>
        [System.SerializableAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
        public partial class searchEventImage
        {

            private string urlField;

            private long widthField;

            private long heightField;

            private object captionField;

            private searchEventImageThumb thumbField;

            private searchEventImageSmall smallField;

            private searchEventImageMedium mediumField;

            /// <remarks/>
            public string url
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
            public long width
            {
                get
                {
                    return this.widthField;
                }
                set
                {
                    this.widthField = value;
                }
            }

            /// <remarks/>
            public long height
            {
                get
                {
                    return this.heightField;
                }
                set
                {
                    this.heightField = value;
                }
            }

            /// <remarks/>
            public object caption
            {
                get
                {
                    return this.captionField;
                }
                set
                {
                    this.captionField = value;
                }
            }

            /// <remarks/>
            public searchEventImageThumb thumb
            {
                get
                {
                    return this.thumbField;
                }
                set
                {
                    this.thumbField = value;
                }
            }

            /// <remarks/>
            public searchEventImageSmall small
            {
                get
                {
                    return this.smallField;
                }
                set
                {
                    this.smallField = value;
                }
            }

            /// <remarks/>
            public searchEventImageMedium medium
            {
                get
                {
                    return this.mediumField;
                }
                set
                {
                    this.mediumField = value;
                }
            }
        }

        /// <remarks/>
        [System.SerializableAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
        public partial class searchEventImageThumb
        {

            private string urlField;

            private long widthField;

            private long heightField;

            /// <remarks/>
            public string url
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
            public long width
            {
                get
                {
                    return this.widthField;
                }
                set
                {
                    this.widthField = value;
                }
            }

            /// <remarks/>
            public long height
            {
                get
                {
                    return this.heightField;
                }
                set
                {
                    this.heightField = value;
                }
            }
        }

        /// <remarks/>
        [System.SerializableAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
        public partial class searchEventImageSmall
        {

            private string urlField;

            private long widthField;

            private long heightField;

            /// <remarks/>
            public string url
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
            public long width
            {
                get
                {
                    return this.widthField;
                }
                set
                {
                    this.widthField = value;
                }
            }

            /// <remarks/>
            public long height
            {
                get
                {
                    return this.heightField;
                }
                set
                {
                    this.heightField = value;
                }
            }
        }

        /// <remarks/>
        [System.SerializableAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
        public partial class searchEventImageMedium
        {

            private string urlField;

            private long widthField;

            private long heightField;

            /// <remarks/>
            public string url
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
            public long width
            {
                get
                {
                    return this.widthField;
                }
                set
                {
                    this.widthField = value;
                }
            }

            /// <remarks/>
            public long height
            {
                get
                {
                    return this.heightField;
                }
                set
                {
                    this.heightField = value;
                }
            }
        }
    }