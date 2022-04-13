using System;
using System.Collections.Generic;
using mtshopcc.Views;
using Xamarin.Forms;

namespace mt_shop_cc
{
    public partial class AppShell : Xamarin.Forms.Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute("details", typeof(Details));
            Routing.RegisterRoute("subcategories", typeof(Categories));
            Routing.RegisterRoute("articles", typeof(ArticleList));
        }

    }
}
