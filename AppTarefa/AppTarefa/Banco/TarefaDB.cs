using AppTarefa.Modelos;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppTarefa.Banco
{
    public class TarefaDB
    {
        private BancoContext Banco { get; set; }

        public TarefaDB()
        {
            Banco = new BancoContext();
        }

        public async Task<List<Tarefas>> PesquisarAsync(DateTime data)
        {
            return await Banco.Tarefas.Where(a =>
            a.Data.HasValue && a.Data.Value.Year == data.Year
            && a.Data.Value.Month == data.Month
            && a.Data.Value.Day == data.Day).ToListAsync();
        } 

        public async Task<bool> CadastrarAsync(Tarefas tarefa)
        {
            Banco.Tarefas.Add(tarefa);
            int linhas = await Banco.SaveChangesAsync();

            //Verifica se alguma linha foi adicionada. Se for igual a 0, significa que nada foi salvo
            return (linhas > 0) ? true : false;
        }

        public async Task<bool> AtualizarAsync(Tarefas tarefa)
        {
            Banco.Tarefas.Update(tarefa);
            int linhas = await Banco.SaveChangesAsync();

            //Verifica se alguma linha foi alterada. Se for igual a 0, significa que nada foi salvo
            return (linhas > 0) ? true : false;
        }

        public async Task<bool> ExcluirAsync(int id)
        {
            Tarefas tarefa = await ConsultarAsync(id);
            Banco.Tarefas.Remove(tarefa);
            int linhas = await Banco.SaveChangesAsync();

            //Verifica se alguma linha foi adicionada. Se for igual a 0, significa que nada foi salvo
            return (linhas > 0) ? true : false;
        }

        public async Task<Tarefas> ConsultarAsync(int id)
        {
            return await Banco.Tarefas.FindAsync(id);
        }

    }
}
