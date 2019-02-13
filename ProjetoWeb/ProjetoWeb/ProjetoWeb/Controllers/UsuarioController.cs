using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ProjetoWeb.Models;


namespace ProjetoWeb.Controllers
{
    public class UsuarioController : Controller
    {
        private SistemaCadastroEntities1 contexto = new SistemaCadastroEntities1();

        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(FormCollection formLogin)
        {
            string Login = Request.Form["Login"];
            string Senha = Request.Form["Senha"];

            if (ModelState.IsValid)
            {
                if (AutenticarLogin(Login, Senha) == true)
                {

                    return RedirectToAction("../Home/Index");
                }
                else
                {
                    ModelState.AddModelError("", "Usuário ou senha incorretos.");

                }
            }
            return View();
        }


        [HttpGet]
        public ActionResult Cadastrar()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Cadastrar(usuario _usuario)
        {
            _usuario.cpf = !string.IsNullOrWhiteSpace(Request.Form["cpf"]) ? Convert.ToInt64(Request.Form["cpf"].Replace(".", "").Replace("-", "")) : 0;

            //if (ModelState.IsValid)
            //{
                if (!VerificarLogin(_usuario.loginUsuario) && !VerificarCPF(_usuario.cpf))
                {
                    contexto.usuario.Add(_usuario);
                    contexto.SaveChanges();
                    ModelState.Clear();
                    _usuario = null;
                    return RedirectToAction("Consultar");
                }
                else
                {
                    ModelState.AddModelError("", "Usuário já cadastrado");
                                        
                }
            //}
            return View(_usuario);
        }


        [HttpGet]
        public ActionResult Consultar()
        {
            List<usuario> listaUsuario = new List<usuario>();
            listaUsuario = contexto.usuario.ToList();
            return View(listaUsuario);
        }

        [HttpGet]
        public ActionResult Editar(int id)
        {

            usuario _usuario = contexto.usuario.Find(id);

            if (_usuario == null)
            {
                return HttpNotFound();
            }

            return View(_usuario);
        }

        [HttpPost, ActionName("Editar")]
        [ValidateAntiForgeryToken]
        public ActionResult ConfirmarEditar(usuario _usuario)
        {
            //usuario _usuario = contexto.usuario.Find(id);
            contexto.Entry(_usuario).State = EntityState.Modified;
            contexto.SaveChanges();
            return RedirectToAction("Consultar");
        }

        [HttpGet]
        public ActionResult Excluir(int id)
        {
            usuario _usuario = contexto.usuario.Find(id);

            if (_usuario == null)
            {
                return HttpNotFound();
            }

            return View(_usuario);
        }

        

        [HttpPost, ActionName("Excluir")]
        [ValidateAntiForgeryToken]
        public ActionResult ConfirmarExcluir(int id)
        {
            usuario _usuario = contexto.usuario.Find(id);
            contexto.usuario.Remove(_usuario);
            contexto.SaveChanges();
            return RedirectToAction("Consultar");  
        }


        //[HttpGet]
        //public ActionResult Pesquisar()
        //{
        //    return View();
        //}

        //[HttpGet]
        public ActionResult Pesquisar(FormCollection formPesquisa)
        {
            string Nome = Request.Form["Nome"];
            string Sobrenome = Request.Form["Sobrenome"];
            string Email = Request.Form["Email"];
            string Login = Request.Form["Login"];
            string Status = Request.Form["Status"];

            IQueryable<usuario> resultado = contexto.usuario;

            if (!string.IsNullOrEmpty(Nome))
            {
                resultado = resultado.Where(r => r.nome == Nome);
            }

            if (!string.IsNullOrEmpty(Sobrenome))
            {
                resultado = resultado.Where(r => r.sobrenome == Sobrenome);
            }

            if (!string.IsNullOrEmpty(Email))
            {
                resultado = resultado.Where(r => r.email == Email);
            }

            if (!string.IsNullOrEmpty(Login))
            {
                resultado = resultado.Where(r => r.loginUsuario == Login);
            }

            if (!string.IsNullOrEmpty(Status))
            {
                resultado = resultado.Where(r => r.stAtivo.ToString() == Status);
            }

            var lista = resultado.ToList();

            foreach (var item in lista)
            {
                item.cpf_formatado = FormataCpf(item.cpf.ToString());
            }

            return View(lista);
            
        }


        public bool VerificarLogin(string loginUsuario)
        {
            var existeLogin = (from u in contexto.usuario
                               where u.loginUsuario == loginUsuario
                               select u).FirstOrDefault();
            if (existeLogin != null)
                return true;
            else
                return false;
        }

        public bool VerificarCPF(long? cpf)
        {
            var existeCPF = (from u in contexto.usuario
                               where u.cpf == cpf
                             select u).FirstOrDefault();
            if (existeCPF != null)
                return true;
            else
                return false;
        }

        public bool AutenticarLogin(string Login, string Senha)
        {
            var existeLogin = (from u in contexto.usuario
                               where u.loginUsuario == Login && u.senha == Senha 
                               select u).SingleOrDefault();
            if (existeLogin != null)
                return true;
            else
                return false;
        }

        public string FormataCpf(string CPF)
        {
            return Convert.ToUInt64(CPF).ToString(@"000\.000\.000\-00");
        }
    }
 }