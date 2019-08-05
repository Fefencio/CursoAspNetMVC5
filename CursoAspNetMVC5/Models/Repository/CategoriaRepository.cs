using CursoAspNetMVC5.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace CursoAspNetMVC5.Models.Repository
{
    public class CategoriaRepository : ICRUDJsonXml<Categoria>
    {
        private string pathXml = @"C:\\DataSite\\CursoAspNetMVC5.xml";

        public void Create()
        {
            if (!File.Exists(pathXml))
            {
                StringWriter stream = new StringWriter();
                using (XmlWriter xmlWriter = XmlWriter.Create(stream, new XmlWriterSettings()
                { Indent=true, Encoding=System.Text.Encoding.UTF8}))
                {
                    xmlWriter.WriteStartDocument();
                    xmlWriter.WriteStartElement("CursoAspNetMVC5");
                    xmlWriter.WriteStartElement("DadosCriacao");
                    xmlWriter.WriteAttributeString("Criador", "Sistema");
                    xmlWriter.WriteAttributeString("DataCriacao", DateTime.Now.ToString());
                    xmlWriter.WriteEndElement();
                    xmlWriter.WriteEndElement();

                    xmlWriter.Flush();
                }
                File.WriteAllText(pathXml, stream.ToString());
            }
        }

        public void Delete(Categoria obj)
        {

            DataSet ds = new DataSet();

            ds.ReadXml(pathXml);
            int xmlRow = ds.Tables["Categoria"].Rows.IndexOf(ds.Tables["Categoria"]
                .Select(string.Format("CategoriaId='{0}'", obj.CategoriaId)).First());

            ds.Tables["Categoria"].Rows.RemoveAt(xmlRow);
            ds.WriteXml(pathXml);
        }

        public void Insert(Categoria obj)
        {
            Create();
            using (StringReader stringReader = new StringReader(File.ReadAllText(pathXml)))
            {
                XmlDocument xmlDocument = new XmlDocument();
                xmlDocument.Load(stringReader);

                XmlElement categoriaElement = xmlDocument.CreateElement("Categoria");

                XmlElement IDElement = xmlDocument.CreateElement("CategoriaId");
                IDElement.InnerText = obj.CategoriaId.ToString();

                XmlElement NomeElement = xmlDocument.CreateElement("Nome");
                NomeElement.InnerText = obj.Nome;

                XmlElement DescricaoElement = xmlDocument.CreateElement("Descricao");
                DescricaoElement.InnerText = obj.Descricao;

                categoriaElement.AppendChild(IDElement);
                categoriaElement.AppendChild(NomeElement);
                categoriaElement.AppendChild(DescricaoElement);

                xmlDocument.DocumentElement.AppendChild(categoriaElement);

                xmlDocument.Save(pathXml);
            }
        }

        public List<Categoria> Select()
        {
            Create();
            List<Categoria> categorias = new List<Categoria>();

            using (StringReader stringReader = new StringReader(File.ReadAllText(pathXml)))
            {
                using (XmlReader xmlReader = XmlReader.Create(stringReader, new XmlReaderSettings() { IgnoreComments = true }))
                {
                    while (xmlReader.ReadToFollowing("Categoria"))
                    {
                        xmlReader.ReadToFollowing("CategoriaId");
                        int id = Convert.ToInt16(xmlReader.ReadString());
                        xmlReader.ReadToFollowing("Nome");
                        string nome = xmlReader.ReadString();
                        xmlReader.ReadToFollowing("Descricao");
                        string descricao = xmlReader.ReadString();

                        var categoria = new Categoria()
                        {
                            CategoriaId = id,
                            Nome = nome,
                            Descricao = descricao
                        };
                        categorias.Add(categoria);
                    }
                }
            }

            return categorias;
        }

        public void Update(Categoria obj)
        {
            DataSet ds = new DataSet();

            ds.ReadXml(pathXml);
            int xmlRow = ds.Tables["Categoria"].Rows.IndexOf(ds.Tables["Categoria"]
                .Select(string.Format("CategoriaId='{0}'", obj.CategoriaId)).First());

            ds.Tables["Categoria"].Rows[xmlRow]["CategoriaId"] = obj.CategoriaId;

            ds.Tables["Categoria"].Rows[xmlRow]["Nome"] = obj.Nome;

            ds.Tables["Categoria"].Rows[xmlRow]["Descricao"] = obj.Descricao;
            ds.WriteXml(pathXml);
        }
    }
}