import { Pipe, PipeTransform } from '@angular/core';
import { ICow } from './ICow';

@Pipe({
  name: 'filterCows',
})
export class FilterCowsPipe implements PipeTransform {
  transform(cows: ICow[]): ICow[] {
    return cows.filter((c) => c);
  }
}
