﻿using AppTarefa.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AppTarefa.Telas
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Visualizar : ContentPage
    {
        public Visualizar()
        {
            InitializeComponent();
        }

        public Visualizar(Tarefas tarefa)
        {
            InitializeComponent();

            BindingContext = tarefa;
        }

        private void BtnVoltar(object sender, EventArgs e)
        {
            Navigation.PopAsync();
        }
    }
}