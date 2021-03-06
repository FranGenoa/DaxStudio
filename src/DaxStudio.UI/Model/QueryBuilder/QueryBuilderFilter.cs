﻿using ADOTabular;
using Caliburn.Micro;
using DaxStudio.Interfaces;
using DaxStudio.UI.Enums;
using DaxStudio.UI.Events;
using DaxStudio.UI.Model;
using GongSolutions.Wpf.DragDrop;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.Composition;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DaxStudio.UI.Model
{
    public class QueryBuilderFilter : PropertyChangedBase
    {

        public QueryBuilderFilter(IADOTabularColumn obj)
        {
            TabularObject = obj;
        }

        public IADOTabularColumn TabularObject { get; }

        public string Caption => TabularObject.Caption;

        private FilterType _fitlerType;
        public FilterType FilterType 
        {  
            get => _fitlerType;
            set {
                _fitlerType = value;
                NotifyOfPropertyChange(nameof(FilterType));
                NotifyOfPropertyChange(nameof(ShowFilterValue));
            } 
        }

        public IEnumerable<FilterType> FilterTypes
        {
            get
            {
                foreach (FilterType ft in FilterType.GetValues(typeof(FilterType)))
                {
                    switch (ft) {
                        case FilterType.Is:
                        case FilterType.IsNot:
                        case FilterType.IsBlank:
                        case FilterType.IsNotBlank:
                            // the above filters apply to all data types
                            yield return ft;
                            break;
                        case FilterType.StartsWith:
                        case FilterType.DoesNotStartWith:
                        case FilterType.Contains:
                        case FilterType.DoesNotContain:
                            // these filters only apply to strings
                            if (TabularObject.DataType == typeof(string)) yield return ft;
                            break;
                        case FilterType.GreaterThan:
                        case FilterType.GreaterThanOrEqual:
                        case FilterType.LessThan:
                        case FilterType.LessThanOrEqual:
                            // these filters only apply non-strings
                            if (TabularObject.DataType != typeof(string)) yield return ft;
                            break;
                        default:
                            throw new NotSupportedException($"Unknown FilterType '{ft.ToString()}'");
                            break;
                }
                }

                //var items = Enum.GetValues(typeof(FilterType)).Cast<FilterType>();
                //return items;
            }
        }

        public string FilterValue { get; set; }

        public bool ShowFilterValue
        {
            get { return FilterType != FilterType.IsBlank && FilterType != FilterType.IsNotBlank; }
        }

        

    }
}
