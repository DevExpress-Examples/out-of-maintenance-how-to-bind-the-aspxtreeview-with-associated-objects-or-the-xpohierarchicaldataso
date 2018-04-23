using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using DevExpress.Xpo;
using DevExpress.Data.Filtering;

/// <summary>
/// Summary description for XpoHierarchicalDataSourceView
/// </summary>
public class XpoHierarchicalDataSourceView<T> : HierarchicalDataSourceView {
    String viewPath;

    Session session;
    String parentProp;
    String childrenProp;

    public XpoHierarchicalDataSourceView(Session session, String viewPath, String parentProp, String childrenProp) {
        this.session = session;
        this.viewPath = viewPath;
        this.parentProp = parentProp;
        this.childrenProp = childrenProp;
    }

    public override IHierarchicalEnumerable Select() {
        XPCollection<T> collection = new XPCollection<T>(session);
        collection.Criteria = new NullOperator(new OperandProperty(parentProp));
        /* without a sotring the order of objects might differ */
        collection.Sorting.Add(new SortProperty(session.GetClassInfo(typeof(T)).KeyProperty.Name, DevExpress.Xpo.DB.SortingDirection.Ascending));

        return new ObjectCollection<T>(collection, parentProp, childrenProp);
    }
}