/**
 * @name createPaginationArray
 * @author Experience One AG
 *
 * @param activeIndex                 Current Index of Pagination
 * @param maxPages                    Maximium Amount of Pages to Show
 * @param visiblePagesAroundCurrent   How many Pages should surround the Current Page
 * @param minVisibleItems             Minimal Visible Pages and Truncation-Dots
 * @returns                           Array with Generated Pagination
 * @description
 * Generate Smart Truncated Pagination from a Range of Pages
 */

const createPaginationArray = (
  activeIndex: number,
  maxPages: number,
  visiblePagesAroundCurrent = 1,
  minVisibleItems = 7,
): (number | string)[] => {
  const range = [];

  /**
   * If Amount of Pages is <= minimal visible Pages, show all Items
   */
  if (maxPages <= minVisibleItems) {
    return Array.from({ length: maxPages + 1 }, (_, i) => i).slice(1);
  }

  /**
   * Fill Array with activeIndex and the Surrounding Pages
   */
  for (
    let i = Math.max(2, activeIndex - visiblePagesAroundCurrent);
    i <= Math.min(maxPages - 1, activeIndex + visiblePagesAroundCurrent);
    i += 1
  ) {
    range.push(i);
  }

  /**
   * Add Truncation-Dots to the Beginning of the Array if activeIndex is High Enough.
   * We Use 3 as a Decider Because when the Value is < 3 we don't want to Show
   * Truncation-Dots at the Array's Beginning.
   *
   * Example:
   * activeIndex - visiblePagesAroundCurrent = 3;
   * The Array at this point would be [2,3,4].
   * If we would Add Truncation-Dots now, we would get ['...', 2, 3, 4] which is wrong,
   * because it ends up with [1, '...', 2, 3, 4].
   */
  if (activeIndex - visiblePagesAroundCurrent > 3) {
    range.unshift('...');
  }

  /**
   * Add Truncation-Dots to the End of the Array if activeIndex is Low Enough.
   */
  if (activeIndex + visiblePagesAroundCurrent < maxPages - 2) {
    range.push('...');
  }

  /**
   * The pagination must always show N Items.
   * N = minVisibleItems - (2 * visiblePagesAroundCurrent).
   * If Length of Array is < N, fill up Array with Missing Pages.
   */
  if (range.length < minVisibleItems - 2 * visiblePagesAroundCurrent) {
    if (activeIndex < minVisibleItems - 2 * visiblePagesAroundCurrent) {
      /**
       * If activeIndex is low enough, fill up the Array from 1 - N
       * and add Truncation-Dots as well as maxPages-Value (aka Last Page).
       * N = minVisibleItems - 2.
       * - 2 Because of the 2 Items (Dots, maxPages) that will be Added After
       * Filling the Array with Missing Pages.
       */

      range.length = 0;

      for (let i = 1; i <= minVisibleItems - 2; i += 1) {
        range.push(i);
      }

      range.push('...');
      range.push(maxPages);

      return range;
    }

    /**
     * If activeIndex is _NOT_ low enough, fill up the Array from N - maxPages
     * and add Truncation-Dots as well as the First Page.
     */

    range.length = 0;
    range.push(1, '...');

    const arrayBaseLength = range.length;

    for (
      let i = maxPages;
      i > maxPages - (minVisibleItems - arrayBaseLength);
      i -= 1
    ) {
      range.splice(arrayBaseLength, 0, i);
    }

    return range;
  }

  range.unshift(1);
  range.push(maxPages);

  return range;
};

export default createPaginationArray;
