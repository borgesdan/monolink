using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework.Common;

namespace Microsoft.Xna.Framework.EntitySystem
{
    /// <summary>
    /// Classe que agrupa os elementos de uma proprietário.
    /// </summary>
    public class ElementCollection<ELEMENT, OWNER> 
        where ELEMENT : IOwner<OWNER>, INameable
        where OWNER : class
    {
        readonly OWNER owner;
        readonly List<ELEMENT> elements = new List<ELEMENT>();

        /// <summary>
        /// Obtém a quantidade de elementos adicionados.
        /// </summary>
        public int Count => elements.Count;

        //construtor
        internal ElementCollection(OWNER owner)
        {
            this.owner = owner;
        }

        //construtor de cópia
        //destinationEntity é a entidade no qual os componentes serão adicionados
        internal ElementCollection(OWNER destination, ElementCollection<ELEMENT, OWNER> source)
        {
            this.owner = destination;

            for (int i = 0; i < source.Count; i++)
            {
                //ELEMENT ec = (ELEMENT)Activator.CreateInstance(typeof(ELEMENT), source.elements[i]);
                
                ELEMENT ec = (ELEMENT)Activator.CreateInstance(source.elements[i].GetType(), source.elements[i]);
                //EntityComponent ec = new EntityComponent(source.components[i]);
                //ec.Entity = destinationEntity;
                //this.components.Add(ec);
                Add(ec);
            }                       
        }

        /// <summary>
        /// Indexador para busca de um elemento baseado no index que se encontra na lista.
        /// </summary>
        public ELEMENT this[int index] => elements[index];

        /// <summary>
        /// Adiciona um novo elemento à lista de elementos.
        /// </summary>
        /// <param name="element">O elemento a ser adicionado.</param>
        public T Add<T>(T element) where T : ELEMENT
        {
            element.Owner = owner;
            elements.Add(element);
            return element;
        }

        /// <summary>
        /// Adiciona um novo elemento informando o tipo genérico.
        /// </summary>
        /// <typeparam name="T">O tipo do elemento.</typeparam>
        public T Add<T>() where T : ELEMENT
        {
            var comp = Activator.CreateInstance<T>();
            Add(comp);

            return comp;
        }

        /// <summary>Remove o primeiro elemento encontrado do tipo específicado.</summary>
        /// <typeparam name="T">O tipo do elemento.</typeparam>
        public void Remove<T>() where T : ELEMENT
        {
            T cmp = Get<T>();

            if (cmp != null)
                elements.Remove(cmp);
        }

        /// <summary>Remove um elemento pelo seu nome.</summary>
        /// <param name="name">O nome do elemento</param>
        public void RemoveByName(string name)
        {
            var cmp = elements.Find(x => x.Name.Equals(name));

            if (cmp != null)
                elements.Remove(cmp);
        }

        /// <summary>Obtém todos os elementos encontrados do tipo informado.</summary>
        /// <typeparam name="T">O tipo do elemento.</typeparam>
        public List<T> GetAll<T>() where T : ELEMENT
        {
            //obtém o tipo de T
            var type = typeof(T);
            //encontra todos os componentes disponíveis que são do mesmo tipo
            var cmps = elements.FindAll(x => x.GetType().Equals(type));

            //Cria uma lista para adicionar os elementos de mesmo tipo
            List<T> temp = new List<T>();

            foreach (var c in cmps)
                temp.Add((T)c);

            return temp;
        }

        /// <summary>Obtém a primeira ocorrência do tipo do elemento informado.</summary>
        /// <typeparam name="T">O tipo do elemento.</typeparam>
        public T Get<T>() where T : ELEMENT
        {
            //obtém o tipo de T
            var type = typeof(T);
            //encontra o componente
            var cmp = elements.Find(x => x.GetType().Equals(type));

            return (T)cmp;
        }

        /// <summary>
        /// Obtém o primeiro elemento encontrado com o mesmo nome informado.
        /// </summary>
        /// <typeparam name="T">O tipo do elemento.</typeparam>
        /// <param name="name">O nome do elemento.</param>
        public T GetByName<T>(string name) where T : ELEMENT
        {
            var cmp = elements.Find(x => x.Name.Equals(name));

            if (cmp != null)
                return (T)cmp;
            else
                return default;
        }

        /// <summary>
        /// Obtém a lista completa de todos os elemento da entidade.
        /// </summary>
        public List<ELEMENT> GetList()
        {
            return elements;
        }

        /// <summary>
        /// Limpa todos os elemento.
        /// </summary>
        public void Clear()
        {
            elements.Clear();
        }
    }
}
