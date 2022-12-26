import React, {Component} from 'react';
import {Prism as SyntaxHighlighter} from 'react-syntax-highlighter';
import {dark} from 'react-syntax-highlighter/dist/esm/styles/prism';

export class SampleHighlighter extends Component {
  constructor(props) {
    super(props);
    this.codeString = 'import React, { Component } from \'react\';\n' +
      'import { Prism as SyntaxHighlighter } from \'react-syntax-highlighter\';\n' +
      'import { dark } from \'react-syntax-highlighter/dist/esm/styles/prism\';\n' +
      'export class SampleHighlighter extends Component {\n' +
      '    constructor(props) {\n' +
      '        super(props);\n' +
      '        this.codeString = \'(num) => num + 1\';\n' +
      '    }\n' +
      '    render() {\n' +
      '        return (\n' +
      '            <SyntaxHighlighter language="javascript" style={dark}>\n' +
      '                {this.codeString}\n' +
      '            </SyntaxHighlighter>\n' +
      '        );\n' +
      '    }\n' +
      '}\n' +
      '\n';
  }

  render() {
    return (
      <SyntaxHighlighter language="jsx" style={dark}>
        {this.codeString}
      </SyntaxHighlighter>
    );
  }
}

