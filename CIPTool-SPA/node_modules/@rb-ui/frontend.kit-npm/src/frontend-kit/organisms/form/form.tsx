import * as React from 'react';

class FormProps {
  children?: React.ReactNode;
}

const Form: React.FunctionComponent<FormProps> = ({ children }) => (
  <div className="o-form">
    <form aria-label="Example form description">{children}</form>
  </div>
);

export { Form, FormProps };
