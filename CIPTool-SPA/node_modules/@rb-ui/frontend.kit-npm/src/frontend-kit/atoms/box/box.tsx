import * as React from 'react';
import BoxComponent from './index';

/**
 * properties for a box
 */
class BoxProps {
  // a CSS width property string
  width?: string;
  // a CSS height property string
  height?: string;
  // if true, render a modal
  modal?: boolean;
  // the child nodes of the component
  children?: React.ReactNode;
  // only for modals - an optional ID
  modalId?: string;
}

const Box: React.FunctionComponent<BoxProps> = ({
  modal = false,
  width = 'auto',
  height = 'auto',
  children,
  modalId,
}) => {
  const needsStyleAttribute = width !== 'auto' || height !== 'auto';
  const styleAttribute = {
    width,
    height,
  };

  if (modal) {
    return (
      <div
        className="a-box--modal"
        id={modalId ? BoxComponent.modalId(modalId) : null}
      >
        <div
          className="a-box -floating"
          style={needsStyleAttribute ? styleAttribute : null}
        >
          {children}
        </div>
      </div>
    );
  }
  return (
    <div
      className="a-box -floating"
      style={needsStyleAttribute ? styleAttribute : null}
    >
      {children}
    </div>
  );
};

export { Box, BoxProps };