import { Pipe, PipeTransform } from '@angular/core';
import { ICow } from './ICow';

@Pipe({
  name: 'filterCows',
})
export class FilterCowsPipe implements PipeTransform {
  transform(
    cows: ICow[],
    nameFilter: string = '',
    originFilter: string = '',
    purposeFilter: string = ''
  ): ICow[] {
    if (nameFilter === '' && originFilter === '' && purposeFilter === '')
      return cows;

    [nameFilter, originFilter, purposeFilter].forEach((s) => s.toLowerCase());

    return cows.filter(
      (c) =>
        c.name.toLowerCase().includes(nameFilter) &&
        c.origin.toLowerCase().includes(originFilter) &&
        c.purpose.find((s) => s.toLowerCase().includes(purposeFilter)) != null
    );
  }
}
