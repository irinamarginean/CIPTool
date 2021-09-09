import * as React from 'react';

class RadioButtonProps {
  id: string;
  label: string;
  value?: string;
  inputName?: string;
  disabled?: boolean;
  checked?: boolean;
}

/**
 * @name    a-radioButton
 * @type    atom
 * @author Experience One AG
 * @copyright Robert Bosch GmbH
 *
 * @param   {string} id           ID of the Input Field
 * @param   {string} label        Label to show next to Radio-Button
 * @param   {string} value        Value of Radio-Button
 * @param   {string} inputName    Optional Name of Input-Group
 * @param   {boolean} disabled    Disable input field
 * @param   {boolean} checked     Check input field
 * @description
 * representation of radio buttons
 */
const RadioButton: React.FunctionComponent<RadioButtonProps> = ({
  id,
  label,
  value,
  inputName,
  disabled,
  checked,
}: RadioButtonProps) => (
  <>
    <div className="a-radio-button">
      <input
        type="radio"
        id={`radio-button-${id}`}
        value={value}
        name={inputName}
        disabled={disabled}
        checked={checked}
      />
      <label htmlFor={`radio-button-${id}`}>{label}</label>
    </div>
  </>
);

export { RadioButton, RadioButtonProps };
