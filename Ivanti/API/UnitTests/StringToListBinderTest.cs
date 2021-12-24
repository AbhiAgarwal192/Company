using Ivanti.Binders;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Routing;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace UnitTests
{
    public class StringToListBinderTest
    {
        private readonly StringToListBinder _stringToListBinder;

        public StringToListBinderTest()
        {
            _stringToListBinder = new StringToListBinder();
        }

        [Fact]
        public async Task WhenBindingContextIsNull_ThenArgumentExceptionIsThrown()
        {
            await Assert.ThrowsAsync<ArgumentNullException>(() => _stringToListBinder.BindModelAsync(null));
        }

        [Fact]
        public async Task WhenCoordinatesIsNullOrEmpty_ThenCompletedTaskIsReturned()
        {

            var modelBindingContext = new DefaultModelBindingContext();
            var bindingSource = new BindingSource("","",false,false);
            var routeValueDictionary = new RouteValueDictionary
            {
            };
            modelBindingContext.ValueProvider = new RouteValueProvider(bindingSource,routeValueDictionary);

            var task =  _stringToListBinder.BindModelAsync(modelBindingContext);

            Assert.True(task.IsCompleted);
            Assert.False(modelBindingContext.Result.IsModelSet);
            
        }

        [Fact]
        public async Task WhenCoordinatesHasIncorrectValue_ThenCompletedTaskIsReturnedWithEmptyModel()
        {

            var modelBindingContext = new DefaultModelBindingContext();
            var bindingSource = new BindingSource("", "", false, false);
            var routeValueDictionary = new RouteValueDictionary
            {
                {"coordinates", "12"},
            };
            modelBindingContext.ValueProvider = new RouteValueProvider(bindingSource, routeValueDictionary);

            var task = _stringToListBinder.BindModelAsync(modelBindingContext);

            Assert.True(task.IsCompleted);
            Assert.True(modelBindingContext.Result.IsModelSet);
            var list = modelBindingContext.Result.Model as List<int[]>;
            Assert.Empty(list);
        }

        [Fact]
        public async Task WhenCoordinatesIsValid_ThenCompletedTaskIsReturnedWithModel()
        {
            var modelBindingContext = new DefaultModelBindingContext();
            var bindingSource = new BindingSource("", "", false, false);
            var routeValueDictionary = new RouteValueDictionary
            {
                {"coordinates", "[{50,50},{60,50},{60,60}]"},
            };
            modelBindingContext.ValueProvider = new RouteValueProvider(bindingSource, routeValueDictionary);

            var task = _stringToListBinder.BindModelAsync(modelBindingContext);

            Assert.True(task.IsCompleted);
            Assert.True(modelBindingContext.Result.IsModelSet);
            var list = modelBindingContext.Result.Model as List<int[]>;
            Assert.NotEmpty(list);
            Assert.Equal(3,list.Count);
        }
    }
}
