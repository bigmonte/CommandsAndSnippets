export class LogMessage {
  constructor (message, isError) {
    this.message = message // Preferably a String
    this.isError = isError // Preferably a Boolean
    this.id = Math.random().toString(36).substr(2, 7) // Generate random string with 7 chars
  }
}

export class Command {
  constructor (howTo, platform, commandLine, id) {
    this.howTo = howTo
    this.platform = platform
    this.commandLine = commandLine
    this.id = id
  }
}

export class Snippet {
  constructor (howTo, platform, codeSnippet, id) {
    this.howTo = howTo
    this.platform = platform
    this.codeSnippet = codeSnippet
    this.id = id
  }
}
