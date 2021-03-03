import { Node, ObjectNode, ArrayNode, ScalarNode } from "./parser";

export function convertNodeToJsValue(root: Node) {
  if (root instanceof ScalarNode) {
    return scalarToJsValue(root);
  }

  if (root instanceof ObjectNode) {
    const result: { [k: string]: any } = {};

    for (const [key, value] of root.entries) {
      result[key] = convertNodeToJsValue(value);
    }

    return result;
  }

  if (root instanceof ArrayNode) {
    throw new Error("Not implemented");
  }

  throw new Error(`Unknown node type "${root.constructor.name}"`);
}

function scalarToJsValue(node: ScalarNode) {
  const { type, value } = node;

  switch (type) {
    case "boolean":
      if (value === "true") {
        return true;
      }

      if (value === "false") {
        return false;
      }

      throw new Error("Invalid boolean value");
    case "number":
      return parseInt(value, 10);
    case "string":
      return value;
    default:
      throw new Error(`Unknown node of type "${type}" cannot be converted to a js value`);
  }
}