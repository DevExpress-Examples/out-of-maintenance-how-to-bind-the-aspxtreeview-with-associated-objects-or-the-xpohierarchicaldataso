using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using DevExpress.Xpo;

/// <summary>
/// Summary description for XpoHierarchicalDataSource
/// </summary>
public class XpoHierarchicalDataSource<T> : IHierarchicalDataSource {
    public Session Session { get; set; }

    public String ParentProperty { get; set; }
    public String ChildrenProperty { get; set; }

    XpoHierarchicalDataSourceView<T> view;

    public XpoHierarchicalDataSource() { }

    public event EventHandler DataSourceChanged;

    public HierarchicalDataSourceView GetHierarchicalView(string viewPath) {
        if (view == null)
            view = new XpoHierarchicalDataSourceView<T>(Session, viewPath, ParentProperty, ChildrenProperty);

        return view;
    }
}