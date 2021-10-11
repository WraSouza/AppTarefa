using AppTarefa.Banco;
using AppTarefa.Modelos;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AppTarefa.Telas
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Listar : ContentPage
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private ObservableCollection<Tarefas> _lista;

        public ObservableCollection<Tarefas> Lista
        {
            get
            {
                return _lista;
            }
            set
            {
                _lista = value;
                NotifyPropertyChanged("Lista");
            }
        }

        public Listar()
        {
            InitializeComponent();

            //Para evitar travamento na tela
            Task.Run( () => {

                Device.BeginInvokeOnMainThread(async () =>
                {
                    CVListaDeTarefas.ItemsSource = await new TarefaDB().PesquisarAsync(DateTime.Now);
                });
               
            });

            MessagingCenter.Subscribe<Listar, Tarefas>(this, "OnTarefaCadastrada", (sender, tarefa) =>
            {
                if (Lista != null)
                {
                    Lista.Add(tarefa);
                }
                
            });
        }

        private void BtnCadastrar(object sender, EventArgs e)
        {
            Navigation.PushModalAsync(new Telas.Cadastrar());
        }

        private void BtnVisualizar(object sender, EventArgs e)
        {
            var evento = (TappedEventArgs)e;

            var tarefa = (Tarefas)evento.Parameter;            

            Navigation.PushAsync(new Visualizar(tarefa));
        }

        private async void BtnExcluir(object sender, EventArgs e)
        {
            bool pergunta = await DisplayAlert("Excluir","Deseja Realmente Excluir Este Registro?", "Sim", "Não");

            if (pergunta)
            {
                //TODO - Excluir Tarefa do Banco
                var swipeItem = (SwipeItem)sender;
                Tarefas tarefa = (Tarefas)swipeItem.CommandParameter;

                var excluido = await new TarefaDB().ExcluirAsync(tarefa.Id);

                if (excluido)
                {
                    Lista.Remove(tarefa);
                }
                //TODO - Remover Tarefa da CollectionView
            }
        }
    }
}