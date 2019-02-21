using System;
using System.ComponentModel.DataAnnotations;
using ToursSoft.Data.Interface;

namespace ToursSoft.Data.Models
{
    
    //TO DO: Entity info https://www.lucidchart.com/documents/edit/883a5e11-34ba-43d2-a1bc-13a21e7f2119/0
    
    /// <summary>
    /// Base entity
    /// </summary>
    [Serializable]
    public class Model: IModel
    {
        /// <summary>
        /// Contructor
        /// </summary>
        /// <param name="id">Entity Id</param>
        public Model(Guid id)
        {
            Id = id == Guid.Empty ? Guid.NewGuid() : id;
        }

        internal Model(){}

        /// <summary>
        /// Entity Id
        /// </summary>
        [Key]
        public Guid Id { get; set; }
    }
}