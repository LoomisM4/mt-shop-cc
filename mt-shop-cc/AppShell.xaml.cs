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
            Routing.RegisterRoute("details", typeof(Details)); // 2
            Routing.RegisterRoute("subcategories", typeof(Categories)); // 2
            Routing.RegisterRoute("articles", typeof(ArticleList)); // 2
        }

    }
}

// 6