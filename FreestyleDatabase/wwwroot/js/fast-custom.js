import { createColorPalette, parseColorString } from "./fast-components.min.js";

const designSystemProvider = document.querySelector("fast-design-system-provider");

const hexColor = "#d6be69";
const accentColor = parseColorString(hexColor);
const accentPallete = createColorPalette(accentColor);

designSystemProvider.accentBaseColor = hexColor;
designSystemProvider.accentPalette = accentPallete;

const fontSize = "1.2em";
designSystemProvider.typeRampBaseFontSize = fontSize;
designSystemProvider.designUnit = 4;
designSystemProvider.density = 3;
