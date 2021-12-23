using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ivanti.Binders
{
    public class StringToListBinder : IModelBinder
    {
        public Task BindModelAsync(ModelBindingContext bindingContext)
        {
            if (bindingContext == null)
            {
                throw new ArgumentNullException(nameof(bindingContext));
            }

            var values = bindingContext.ValueProvider.GetValue("coordinates");

            if (values.Length == 0)
            {
                return Task.CompletedTask;
            }

            var result = new List<int[]>();
            int[] coordinate = new int[2];

            int j = 0;
            int num = 0;

            string input = values.FirstValue.Replace(" ","");
            
            for (int i=0;i< input.Length;i++)
            {
                if (input[i] == '[' || input[i] == ']')
                {
                    continue;
                }
                else if (input[i] == '{')
                {
                    coordinate = new int[2];
                    j = 0;
                }
                else if (input[i] == '}')
                {
                    coordinate[j] = num;
                    j = 0;
                    num = 0;
                    result.Add(coordinate);

                }
                else if (input[i] == ',')
                {
                    if (i-1>=0 && input[i-1]!='}')
                    {
                        coordinate[j] = num;
                        j++;
                        num = 0;
                    }
                }
                else if (Char.IsDigit(input[i]))
                {
                    num = num * 10 + input[i] - '0';
                }
                else
                {
                    return Task.CompletedTask;
                }
            }

            bindingContext.Result = ModelBindingResult.Success(result);
            return Task.CompletedTask;

        }
    }
}
