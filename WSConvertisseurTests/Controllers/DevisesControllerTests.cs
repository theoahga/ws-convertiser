using Microsoft.VisualStudio.TestTools.UnitTesting;
using WSConvertisseur.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WSConvertisseur.Models;
using Microsoft.AspNetCore.Http;

namespace WSConvertisseur.Controllers.Tests
{
    [TestClass()]
    public class DevisesControllerTests
    {
        [TestMethod()]
        public void GetDevises_ExistingIdPassed_ReturnsRightItem()
        {
            // Arrange
            DevisesController controller = new DevisesController();
            // Act
            var result = controller.GetById(1);
            // Assert
            Assert.IsInstanceOfType(result, typeof(ActionResult<Devise>), "Pas un ActionResult");
            Assert.IsNull(result.Result, "Erreur est pas null");
            Assert.IsInstanceOfType(result.Value, typeof(Devise), "Pas une Devise");
            Assert.AreEqual(new Devise(1, "Dollar", 1.08), (Devise?)result.Value, "Devises pas identiques");
        }

        [TestMethod()]
        public void GetDevises_404_error()
        {
            // Arrange
            DevisesController controller = new DevisesController();
            // Act
            var result = controller.GetById(5);
            // Assert
            Assert.IsInstanceOfType(result, typeof(ActionResult<Devise>), "Pas un ActionResult");
            Assert.IsInstanceOfType(result.Result, typeof(NotFoundResult), "Pas un NotFoundResult");
            Assert.IsNull(result.Value, "Value n'est pas null");
            Assert.AreEqual(((NotFoundResult)result.Result).StatusCode, StatusCodes.Status404NotFound, "Pas 404");
        }

        [TestMethod()]
        public void GetAll_ReturnsRightItems()
        {
            // Arrange
            DevisesController controller = new DevisesController();
            // Act
            var result = controller.GetAll();
            // Assert
            Assert.IsInstanceOfType(result, typeof(IEnumerable<Devise>), "Pas un IEnumerable");

            List<Devise> devises = new List<Devise>();
            devises.Add(new Devise(1, "Dollar", 1.08));
            devises.Add(new Devise(2, "Franc Suisse", 1.07));
            devises.Add(new Devise(3, "Yen", 120));

            CollectionAssert.Equals(result.ToList(), devises);
        }

        [TestMethod()]
        public void Post_NonExistingIdPassed_ReturnsRightItem()
        {
            // Arrange
            DevisesController controller = new DevisesController();
            // Act
            Devise newDevise = new Devise(4, "CAN", 1.5);
            var result = controller.Post(newDevise);
            // Assert
            Assert.IsInstanceOfType(result, typeof(ActionResult<Devise>), "Pas un ActionResult");
            Assert.IsInstanceOfType(result.Result, typeof(CreatedAtRouteResult), "Pas une CreatedAtRouteResult");

            CreatedAtRouteResult routeResult = (CreatedAtRouteResult)result.Result;
            Assert.AreEqual(routeResult.StatusCode, 201);
            Assert.AreEqual(routeResult.Value, newDevise);
        }

        /* [TestMethod()]
    public void Post_NonNammedDevise()
         {
             // Arrange
             DevisesController controller = new DevisesController();
             // Act
             Devise newDevise = new Devise(4, null, 1.5);
             var result = controller.Post(newDevise);
             // Assert
             Assert.IsInstanceOfType(result, typeof(ActionResult<Devise>), "Pas un ActionResult");
             Assert.IsInstanceOfType(result.Result, typeof(CreatedAtRouteResult), "Pas une CreatedAtRouteResult");

             CreatedAtRouteResult routeResult = (CreatedAtRouteResult)result.Result;
             Assert.AreEqual(routeResult.StatusCode, 201);
             Assert.AreEqual(routeResult.Value, newDevise);
         }*/


        [TestMethod()]
        public void Put_BadRequest()
        {
            // Arrange
            DevisesController controller = new DevisesController();
            // Act
            Devise newDevise = new Devise(4, "CAN", 1.5);
            ActionResult<Devise> result = controller.Put(1, newDevise);
            // Assert
            Assert.IsInstanceOfType(result.Result, typeof(BadRequestResult), "Pas un BadRequestResult");
        }

        [TestMethod()]
        public void Put_NotFoundDevise()
        {
            // Arrange
            DevisesController controller = new DevisesController();
            // Act
            Devise newDevise = new Devise(4, "CAN", 1.5);
            ActionResult<Devise> result = controller.Put(4, newDevise);
            // Assert
            Assert.IsInstanceOfType(result.Result, typeof(NotFoundResult), "Pas un NotFoundResult");
        }

        public void Put_NoContent()
        {
            // Arrange
            DevisesController controller = new DevisesController();
            // Act
            Devise newDevise = new Devise(3, "CAN", 1.5);
            ActionResult<Devise> result = controller.Put(3, newDevise);
            // Assert
            Assert.IsInstanceOfType(result.Result, typeof(NoContentResult), "Pas un NoContentResult");
        }

        [TestMethod()]
        public void Delete_ExistingIdPassed_ReturnsOK()
        {
            // Arrange
            DevisesController controller = new DevisesController();
            // Act
            ActionResult<Devise> result = controller.Delete(1);
            // Assert
            Assert.IsInstanceOfType(result, typeof(ActionResult<Devise>), "Pas un ActionResult");
            Assert.IsNull(result.Value, "La devise a bien été supprimé");
        }

        [TestMethod()]
        public void Delete_NonExistingIdPassed_ReturnsNoContent()
        {
            // Arrange
            DevisesController controller = new DevisesController();
            // Act
            ActionResult<Devise> result = controller.Delete(4);
            // Assert
            Assert.IsInstanceOfType(result.Result, typeof(NoContentResult), "Pas un NoContentResult");
        }
    }
}