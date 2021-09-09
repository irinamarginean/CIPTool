/**
 * @name updateIndicatorState
 *
 * @param indicators                  List of all all indicator elements
 * @param activeIndex                 Current Index of Pagination
 * @description
 * Updates the indicators selected/unselected state after a change of pagination
 */

const CLASS_SELECTED = '-selected';

const updateIndicatorState = (
  indicators: NodeListOf<HTMLElement>,
  activeIndex: number,
): void => {
  indicators.forEach(indicator => {
    const parsed = parseInt(indicator.getAttribute('data-index'), 0);

    if (parsed === activeIndex) {
      indicator.classList.add(CLASS_SELECTED);
    } else if (indicator.classList.contains(CLASS_SELECTED)) {
      indicator.classList.remove(CLASS_SELECTED);
    }
  });
};

export default updateIndicatorState;
