enum ArrowPosition {
  TOP_LEFT = 'top-left',
  TOP_CENTER = 'top-center',
  TOP_RIGHT = 'top-right',
  LEFT_TOP = 'left-top',
  LEFT_CENTER = 'left-center',
  LEFT_BOTTOM = 'left-bottom',
  BOTTOM_LEFT = 'bottom-left',
  BOTTOM_CENTER = 'bottom-center',
  BOTTOM_RIGHT = 'bottom-right',
  RIGHT_BOTTOM = 'right-bottom',
  RIGHT_CENTER = 'right-center',
  RIGHT_TOP = 'right-top'
}

const arrowPositions: ArrowPosition[] = [
  ArrowPosition.TOP_LEFT,
  ArrowPosition.TOP_CENTER,
  ArrowPosition.TOP_RIGHT,
  ArrowPosition.LEFT_TOP,
  ArrowPosition.LEFT_CENTER,
  ArrowPosition.LEFT_BOTTOM,
  ArrowPosition.BOTTOM_LEFT,
  ArrowPosition.BOTTOM_CENTER,
  ArrowPosition.BOTTOM_RIGHT,
  ArrowPosition.RIGHT_BOTTOM,
  ArrowPosition.RIGHT_CENTER,
  ArrowPosition.RIGHT_TOP
];

const arrowClasses = arrowPositions.map(position => `-${position}`);

export { ArrowPosition, arrowClasses, arrowPositions };
