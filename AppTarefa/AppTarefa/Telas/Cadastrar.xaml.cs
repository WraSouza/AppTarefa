using AppTarefa.Banco;
using AppTarefa.Modelos;
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
    public partial class Cadastrar : ContentPage
    {
        public Cadastrar()
        {
            InitializeComponent();
        }

        private void FecharModal(object sender, EventArgs e)
        {
            Navigation.PopModalAsync();
        }

        private async void SalvarTarefa(object sender, EventArgs e)
        {
            //Criar Tarefa
            Tarefas tarefa = new Tarefas();
            tarefa.Nome = Nome.Text;
            tarefa.Nota = Nota.Text;
            tarefa.Data = Data.Date;
            tarefa.HorarioInicial = HorarioInicial.Time;
            tarefa.HorarioFinal = HorarioFinal.Time;
            tarefa.Finalizado = false;   
            
            if(await ValidacaoAsync(tarefa))
            {
                //Salvar a Tarefa no banco
                if(await new TarefaDB().CadastrarAsync(tarefa))
                {
                    //Envia Tarefa Cadastrada para tela de listagem e fecha o Modal
                    MessagingCenter.Send<Listar, Tarefas>(new Listar(), "OnTarefaCadastrada", tarefa);

                    //Fecha o Modal
                    await Navigation.PopModalAsync();
                }
            }
            
        }

        //Validação dos dados
        private async Task<bool> ValidacaoAsync(Tarefas tarefa)
        {
            bool validacao = true;

            return validacao;
        }
    }
}