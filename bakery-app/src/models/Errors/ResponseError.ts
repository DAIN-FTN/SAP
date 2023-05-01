export class ResponseError extends Error {
    statusCode = 400;
  
    constructor(message: string) {
      super(message);
  
      // extending a built-in class
      Object.setPrototypeOf(this, ResponseError.prototype);
    }

    setStatus(status: number){
      this.statusCode = status;
    }
  
    getErrorMessage() {
      return 'Something went wrong: ' + this.message;
    }
  }