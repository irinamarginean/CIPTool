import createPaginationArray from './createPaginationArray';

/**
 * @name updatePagination
 *
 * @param activeIndex                 Current Index of Pagination
 * @param maxPages                    Maximium Amount of Pages to Show
 * @param indicators                  List of all all indicator elements
 * @description
 * Updates the numbers of the pagination when active index was changed.
 */

const updatePagination = (
  activeIndex: number,
  maxPages: number,
  indicators: NodeListOf<HTMLElement>,
): void => {
  const paginationArray = createPaginationArray(activeIndex, maxPages);

  paginationArray.forEach((page, index) => {
    if (typeof page === 'number') {
      indicators[index].querySelector('span').textContent = page.toString();
      indicators[index].setAttribute('data-index', page.toString());
    } else if (typeof page === 'string') {
      indicators[index].querySelector('span').textContent = '...';
      indicators[index].removeAttribute('data-index');
    }
  });
};

export default updatePagination;
